#ifndef Motor_h
#define Motor_h

#include "Arduino.h"

#define MOTOR1(x) Motor1.set(x)
#define MOTOR2(x) Motor2.set(x)

class Motor
{
public:
	void set(int speed);
};

#endif