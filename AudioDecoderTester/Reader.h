#pragma once

#define SUBDIV 22
#define INTERRUPTRATE (44100/SUBDIV)
#define LONGFREQ 500
#define SHORTFREQ 1000

#define DEBOUNCE 2
#define LONGPULSE (INTERRUPTRATE/ LONGFREQ)
#define SHORTPULSE (INTERRUPTRATE/ SHORTFREQ)

#define SYNCLOST 2
#define SYNCING 3
#define SYNCED 1

class AudioReader
{
public:
	AudioReader()
	{
		LastVal = 0;
		TimeSinceLastUpCrossing = 0;
		CurrentBit = 7;
		Bytes = 0;
		CurrentVal = 0;
		Sync = SYNCLOST;
		CountDownStarted = false;
		printf("Rate: %d LongPulse: %d ShortPulse: %d\n", (int)INTERRUPTRATE, (int)LONGPULSE, (int)SHORTPULSE);
	}

	bool CountDownStarted;
	short LastVal;
	int TimeSinceLastUpCrossing;

	int CurrentBit;
	int CurrentVal;
	int Bytes;
	int Sync;
	int LastSyncByte;

	void Update(short NewVal)
	{
		TimeSinceLastUpCrossing++;
		if (TimeSinceLastUpCrossing > LONGPULSE * 10)
		{
			Sync = SYNCLOST;
		}
		if (LastVal <= 0 && NewVal > 0)
		{
			//	printf("cycles: %d\n", TimeSinceLastUpCrossing);
			if (TimeSinceLastUpCrossing < DEBOUNCE)
			{
				// skip this..
			}
			else
			{
				bool Bit = false;

				if (TimeSinceLastUpCrossing >= LONGPULSE)
				{
					Bit = true;
				}

				Sleep(5);
				
				switch (Sync)
				{
					case SYNCLOST:
					{
						CurrentVal = (CurrentVal << 1) & 0xff;
						if (Bit) CurrentVal++;
					
						if (CurrentVal == 0x02)
						{
							Sync = SYNCING;
							LastSyncByte = 0x02;
							CurrentBit = 0;
							CountDownStarted = false;
							printf("!");
						}
						else
						{
							printf("?");
						}
					}
				break;
				case SYNCING:
					CurrentVal = (CurrentVal << 1) & 0xff;
					if (Bit) CurrentVal++;
					CurrentBit = (CurrentBit + 1) % 8;
					if (CurrentBit == 0)
					{
						printf("Syncing: %2X\n", CurrentVal);
						if (LastSyncByte == 0x02 && CurrentVal == 0x02)
						{
							// still waiting
						}
						else
						{
							
							if (LastSyncByte == 0x02 && CurrentVal == 0x09)
							{
								CountDownStarted = true;
							}
							else
							{
								if (CurrentVal > 0 && LastSyncByte == CurrentVal + 1)
								{
									// still counting!
									if (CurrentVal == 1)
									{
										Sync = SYNCED;
										Bytes = 0;
									}
								}
								else
								{
									Sync = SYNCLOST;
									CountDownStarted = false;
								}

							}

						}
						LastSyncByte = CurrentVal;
					
					}
					else
					{
						printf(".");
					}
					
					break;
				case SYNCED:
				{
					CurrentVal = (CurrentVal << 1) & 0xff;
					if (Bit) CurrentVal++;
					CurrentBit = (CurrentBit + 1) % 8;
					if (CurrentBit == 0)
					{
						printf("%2X ", CurrentVal);
						Bytes++;
						if (Bytes % 16 == 0) printf("\n");
						
						CurrentBit = 0;
					}
					break;
				}
				}
			}


			TimeSinceLastUpCrossing = 0;
		}
		LastVal = NewVal;
	}
};
