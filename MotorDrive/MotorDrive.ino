#include "L298.h"
#include"CommandProcesser.h";

CommandProcesser parser;
char myChar;

void Ride(char* command, byte comLen)
{
	Serial.println("_____");
}

void setup()
{
	Serial.begin(115200);
        Serial2.begin(9600);
	parser = CommandProcesser();
	parser.AddCommand("ride", 0, Ride);
}

void loop()
{
	//parser.getBytesFromSerial();
	//parser.recoAll();
        //Serial1.println(123);
        while (Serial2.available()) {
            myChar = Serial2.read();
            Serial.print(myChar);
        }
}
