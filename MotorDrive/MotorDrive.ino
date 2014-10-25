#include "L298.h"
#include"CommandProcesser.h";

CommandProcesser parser;
char myChar;

byte getDigitByChar(char c) 
{
	return (byte) (c - '0');
}

void Ride(char* command, byte comLen)
{
	/*for(int i = 0; i < comLen; i++)
	{
		Serial.print(command[i]);
	}
	Serial.println();*/

	int leftSpeed = 100*getDigitByChar(command[5]) 
					+ 10*getDigitByChar(command[6]) 
					+ getDigitByChar(command[7]);
	if(command[4] == '-')
		leftSpeed *= -1;

	int rightSpeed = 100*getDigitByChar(command[9]) 
					+ 10*getDigitByChar(command[10]) 
					+ getDigitByChar(command[11]);
	if(command[8] == '-')
		rightSpeed *= -1;

	Motor1.set(leftSpeed);
	Motor2.set(rightSpeed);
}

void setup()
{
	Serial.begin(115200);
    Serial2.begin(115200);
	parser = CommandProcesser();
	parser.AddCommand("ride", 8, Ride);
}

void loop()
{
	parser.getBytesFromSerial();
	parser.recoAll();
}
