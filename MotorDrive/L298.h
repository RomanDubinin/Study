#ifndef L298_h
#define L298_h

#include "Arduino.h"
#include "Motor.h"

class L298 : public Motor
{
private:
	uint8_t PIN_PWM;
	uint8_t PIN_IN1;
	uint8_t PIN_IN2;
public:
	L298(uint8_t PIN_PWM, uint8_t PIN_IN1, uint8_t PIN_IN2)
	{
		L298::PIN_PWM = PIN_PWM;
		L298::PIN_IN1 = PIN_IN1;
		L298::PIN_IN2 = PIN_IN2;
		pinMode(PIN_PWM, OUTPUT);
		pinMode(PIN_IN1, OUTPUT);
		pinMode(PIN_IN2, OUTPUT);
		digitalWrite(PIN_PWM, LOW);
		digitalWrite(PIN_IN1, LOW);
		digitalWrite(PIN_IN2, LOW);
	}
	void set(int speed)
	{
		if (speed < 0)
		{
			digitalWrite(PIN_IN2, LOW);
			digitalWrite(PIN_IN1, HIGH);
		}
		else
		{
			digitalWrite(PIN_IN1, LOW);
			digitalWrite(PIN_IN2, HIGH);
		}
		analogWrite(PIN_PWM, min(255, abs(speed)));
	}
};

extern L298 Motor1(9, 8, 7);
extern L298 Motor2(10, 11, 12);

#endif