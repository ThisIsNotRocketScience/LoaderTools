#include <stdio.h>
#include <Windows.h>
#include <conio.h>

#include "Reader.h"
int main(int argc, char **argv)
{
	WAVEHDR HDR;
	FILE *F = fopen(argv[1], "rb+");
	if (!F) return 0;
	fread(&HDR, sizeof(HDR), 1, F);
	short target[1];
	
	AudioReader R;
	byte b;
	//fread(&b, 1, 1, F);
	//for (int i =0 ;i<1;i++) R.Update(0);
	int count = 0;
	while (fread(target, sizeof(short), 1, F) && count < 1000 *SUBDIV * LONGPULSE)
	{
		if (count % (SUBDIV) == 0)
		{
			R.Update(target[0]);
		}
		count++;
	}

	printf("\n\npress any key to quit..\n");
	while (!_kbhit()) { Sleep(5); };
	return 0;
}