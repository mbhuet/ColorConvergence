using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniduino;

public class ArduinoInput : MonoBehaviour {

        public Arduino arduino;

        public int pin = 2;
        public int pinValue;
        public int testLed = 13;

    bool arduinoInitialized;

        void Start()
        {
            arduino = Arduino.global;
            arduino.Log = (s) => Debug.Log("Arduino: " + s);
            arduino.Setup(ConfigurePins);
        }

        void ConfigurePins()
        {
            arduino.pinMode(pin, PinMode.INPUT);
            arduino.reportDigital((byte)(pin / 8), 1);
            // set the pin mode for the test LED on your board, pin 13 on an Arduino Uno
            arduino.pinMode(testLed, PinMode.OUTPUT);
        arduinoInitialized = true;
        }


        void Update()
        {
        if (arduinoInitialized)
        {
            // read the value from the digital input
            pinValue = arduino.digitalRead(pin);
            // apply that value to the test LED
            arduino.digitalWrite(testLed, pinValue);
            if (pinValue == 0) ConvergenceManager.Instance.TranslateColor(ColorGroup.RED, Vector3.right * Time.deltaTime);
        }
        }
    
}
