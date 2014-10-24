// CommandProcesser.h

#ifndef _COMMANDPROCESSER_h
#define _COMMANDPROCESSER_h

#if defined(ARDUINO) && ARDUINO >= 100
#include "Arduino.h"
#else
#include "WProgram.h"
#endif

#define DEBUGPLAINTEXT 1

typedef void(*actionPointer)(char*, byte);
typedef bool(*predicatePointer)(char*, byte);

const int numberOfCommands = 15;
const int lengthOfCommands = 13;

class CommandProcesser
{
private:
	actionPointer* actions;
	predicatePointer* predicates;

	char* ControlWords[numberOfCommands];
	byte ControlWordLength[numberOfCommands];
	byte CommandLength[numberOfCommands];

	byte beginIndexQueue;
	byte endIndexQueue;

	char cycleBuffer[lengthOfCommands];
	char toReadBuffer[lengthOfCommands + 1];

	byte comandsRegistred;

	bool byteAdded;
	bool bytesReaded;
	bool collectionChanged;

	void safeIncrement(byte& target){
		shiftIndex(target, 1);
	}

	void shiftIndex(byte&index, byte shift2){
		index += shift2;
		index %= lengthOfCommands;
	}

	void addByte2Buffer(char target){
		safeIncrement(endIndexQueue);

		if (beginIndexQueue == endIndexQueue)
			safeIncrement(beginIndexQueue);   //lost first(oldest) byte in buffer

		cycleBuffer[endIndexQueue] = target;

		collectionChanged = true;
	}

	void prepare2ReadBytes(byte numberObBytes){
		byte tempIndex = beginIndexQueue;
		safeIncrement(tempIndex);

		for (byte i = 0; i < numberObBytes; i++, safeIncrement(tempIndex)){
			toReadBuffer[i] = cycleBuffer[tempIndex];
		}
		toReadBuffer[numberObBytes] = '\0';
	}

	byte availableBytes(){
		if (endIndexQueue >= beginIndexQueue)
			return (endIndexQueue - beginIndexQueue);
		return (lengthOfCommands - beginIndexQueue + endIndexQueue);
	}

	byte freeBytes(){
		return lengthOfCommands - availableBytes();
	}

	bool is_A_PrefixOf_B_(char* A, char* B, byte charsToTest){
		for (byte i = 0; i < charsToTest; i++){
			if (A[i] != B[i])
				return false;
		}
		return true;
	}

	bool shiftIfNotPrefix(){
		byte available = availableBytes();
		//prepare2ReadBytes(available);
		bool anyIsPrefix = false;

		if (available > 0){
			for (byte i = 0; i < comandsRegistred; i++){
				if (is_A_PrefixOf_B_(toReadBuffer, ControlWords[i], min(ControlWordLength[i], available))){
					anyIsPrefix = true;
				}
			}
			if (!anyIsPrefix){
#ifdef DEBUGPLAINTEXT
				Serial.println("SHIFTING 1");
#endif // DEBUGPLAINTEXT

				shiftIndex(beginIndexQueue, 1);
				return true;
			}
			return false;
		}
	}

public:

	void getBytesFromSerial(){
  		byte toRead = min(Serial1.available(), freeBytes()-1);
		for (byte i = 0; i < toRead; i++){
			addByte2Buffer(Serial1.read());
		}
	}

	void AddCommand(char* controlWord, byte parameterLength, actionPointer act, predicatePointer predicate4Parameter = NULL){

		byte controlWordLength = strlen(controlWord);
		byte commandLength = controlWordLength + parameterLength;

#ifdef DEBUGPLAINTEXT
		Serial.print("CoMmand Adding...  ");
		Serial.print(controlWord);
		Serial.print(";  commandLength: ");
		Serial.println(commandLength);
#endif
		ControlWords[comandsRegistred] = controlWord;
#ifdef DEBUGPLAINTEXT
		Serial.println("Word added");
#endif
		ControlWordLength[comandsRegistred] = controlWordLength;
#ifdef DEBUGPLAINTEXT
		Serial.println("Control Length added");
#endif
		CommandLength[comandsRegistred] = commandLength;
#ifdef DEBUGPLAINTEXT
		Serial.println("Command length added");
#endif
		actions[comandsRegistred] = act;
#ifdef DEBUGPLAINTEXT
		Serial.println("Action added");
#endif
		predicates[comandsRegistred] = predicate4Parameter;
#ifdef DEBUGPLAINTEXT
		Serial.println("Predicate added");
#endif
		comandsRegistred++;
	}

	void AddCommand(char* controlWord, actionPointer act){
		AddCommand(controlWord, 0, act);
	}

	void recoAll(){
		if (collectionChanged){
			byte available = availableBytes();
			prepare2ReadBytes(available);

#ifdef DEBUGPLAINTEXT

			Serial.print("Recognizing: ");
			Serial.println(toReadBuffer);
			Serial.print("State begin = ");
			Serial.print(beginIndexQueue);
			Serial.print(";  end = ");
			Serial.print(endIndexQueue);
			Serial.print(";  available: ");
			Serial.println(available);

#endif // DEBUGPLAINTEXT

			for (byte i = 0; i < comandsRegistred; i++){
				if (ControlWordLength[i] <= available){
#ifdef DEBUGPLAINTEXT
					Serial.print("Command handling: ");
					Serial.println(ControlWords[i]);
#endif
					if (strncmp(toReadBuffer, ControlWords[i], ControlWordLength[i]) == 0){
#ifdef DEBUGPLAINTEXT
						Serial.println("Compared");
#endif
						if (predicates[i] == NULL || predicates[i](toReadBuffer + ControlWordLength[i], min(CommandLength[i], available) - ControlWordLength[i])){
#ifdef DEBUGPLAINTEXT
							Serial.println("Predicated");
#endif
							if (CommandLength[i] <= available){
#ifdef DEBUGPLAINTEXT
								Serial.print("Accepted, command: ");
								Serial.println(ControlWords[i]);
#endif
								actions[i](toReadBuffer, CommandLength[i]);

								shiftIndex(beginIndexQueue, CommandLength[i]);

								collectionChanged = true;
								return;
							}
						}
						else{
							collectionChanged = true;
							safeIncrement(beginIndexQueue);
							available--;
							prepare2ReadBytes(available);
#ifdef DEBUGPLAINTEXT
							Serial.println("Bad predicate shifting");
#endif // DEBUGPLAINTEXT
						}

					}
				}
			}

			collectionChanged = shiftIfNotPrefix();
		}

	}

	void init(){

	}

	CommandProcesser()
	{
		bytesReaded = false;
		byteAdded = false;
		comandsRegistred = 0;
		actions = (actionPointer*)malloc((sizeof (actionPointer*))*numberOfCommands);
		predicates = (predicatePointer*)malloc((sizeof (predicatePointer*))*numberOfCommands);
	}
	~CommandProcesser(){
		free(actions);
	}

};

extern CommandProcesser COMMANDPROCESSER;

#endif
