#include <Adafruit_NeoPixel.h>
#ifdef __AVR__
  #include <avr/power.h>
#endif

#ifdef __arm__
// should use uinstd.h to define sbrk but Due causes a conflict
extern "C" char* sbrk(int incr);
#else  // __ARM__
extern char *__brkval;
#endif  // __arm__



#define PIN        6
#define LEDPIN     13
#define GREENLED 52
#define YELLOWLED 50
#define REDLED 48

#define NUMPIXELS 38

Adafruit_NeoPixel pixels(NUMPIXELS, PIN, NEO_GRB + NEO_KHZ800);
#define DELAYVAL 50

struct Subframe
{
  //unsigned long color; //unsigned long = 4 bytes
  //unsigned long pixelBitmap[4]; //2^arraysize must be greater than the number of pixels
  byte pixelMap[16] = {0};
  byte colorRed;
  byte colorGreen;
  byte colorBlue;
  
  
};

struct Frame
{
  unsigned int subframeCount = 0;
  unsigned int frameDuration = 0; //milliseconds
  Subframe subframes[16];
  
};
  
Frame frames[8];

void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200);
  pixels.begin();

  pinMode(LEDPIN, OUTPUT);
  pinMode(GREENLED, OUTPUT);
  pinMode(REDLED, OUTPUT);
  pinMode(YELLOWLED, OUTPUT);
  
  digitalWrite(LEDPIN, LOW);
  digitalWrite(REDLED, LOW);
  
  delay(5000);
}

void loop() {
  // put your main code here, to run repeatedly:
  //Serial.println(freeMemory());
  pixels.clear();
  if(Serial.available()>0)
  {
    getFrameFromSerial(frames[0]);
    ApplyFrame(frames[0]);
  }
  
  for(int i=0; i<NUMPIXELS; i++)
  {
    clearPixels();
    //pixels.setPixelColor(i, pixels.Color(255, 255, 255));
    pixels.show();
    Serial.println(i);
    delay(2000);
  }
  delay(1000);
  clearPixels();
  /*for(int i=0; i<NUMPIXELS; i++)
  {
    pixels.setPixelColor(i, pixels.Color(0,255,0));
    pixels.show();
    delay(DELAYVAL);
  }*/
}

void ApplyFrames(Frame frameArray[], int frameCount)
{
  for(int i=0; i<frameCount; i++)
  {
    ApplyFrame(frameArray[i]);
    delay(frameArray[i].frameDuration);
  }
}

void ApplyFrame(Frame& frame)
{
  if(frame.subframeCount < 1)
    return;
    
  pixels.clear();
  digitalWrite(REDLED, HIGH);


  for(int i=0; i<frame.subframeCount; i++)
  {
  
    byte colorR = frame.subframes[i].colorRed;
    byte colorG = frame.subframes[i].colorGreen;
    byte colorB = frame.subframes[i].colorBlue;
    
  
    for(int a=0; a<16; a++)
    {
      for(int b=0; b<8; b++)
      {
        if( ((frame.subframes[i].pixelMap[a] >> b) & 1) == 1)
        {
          pixels.setPixelColor(a*8+b, pixels.Color(colorR, colorG, colorB));
        }
      }
    }
  }
  
  pixels.show();
}

void clearPixels()
{
  for(int i=0; i<NUMPIXELS; i++)
  {
    pixels.setPixelColor(i, pixels.Color(0,0,0));
  }
  pixels.show();
}

void getFrameFromSerial(Frame& newFrame)
{
  newFrame.subframeCount = 0;
  if(Serial.available() > 0)
  {
    char incomingByte = Serial.read();
    byte byteBuffer[32];
    
    if(incomingByte == 'C')
    {
      delay(2);
      digitalWrite(LEDPIN, HIGH);

      byte numSubframes = Serial.read();
      Serial.println(numSubframes);

      byte durationBytes[2];
      durationBytes[0]=Serial.read();
      durationBytes[1]=Serial.read();
      
      newFrame.subframeCount = numSubframes;
      newFrame.frameDuration = 1000;

      for(int j=0; j<numSubframes; j++)
      {

        Subframe sframe = newFrame.subframes[j];
        
        byte colorBytes[3];
        int numRead = Serial.readBytes(colorBytes, 3);
        if(numRead != 3)
        {
          Serial.println(numRead);
        }
        
  
        newFrame.subframes[j].colorRed = colorBytes[0];
        newFrame.subframes[j].colorGreen = colorBytes[1];
        newFrame.subframes[j].colorBlue = colorBytes[2];
        byte pixelBitmapBytes[16];
  
        numRead = Serial.readBytes(pixelBitmapBytes, 16);
  
        if(numRead != 16)
        {
          Serial.println("16 bytes not read!");
          Serial.println(numRead);
        }
        
        
        for(int i=0; i<16; i++)
          newFrame.subframes[j].pixelMap[i] = pixelBitmapBytes[i];
        
  
      }

    }
  }
  return;
  //return newFrame;
}

int freeMemory() {
  char top;
#ifdef __arm__
  return &top - reinterpret_cast<char*>(sbrk(0));
#elif defined(CORE_TEENSY) || (ARDUINO > 103 && ARDUINO != 151)
  return &top - __brkval;
#else  // __arm__
  return __brkval ? &top - __brkval : &top - __malloc_heap_start;
#endif  // __arm__
}
