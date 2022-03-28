#include <Adafruit_NeoPixel.h>

//Some LED strips switch the Green and Blue values. Set to false if you are connecting to a Bestech display's LED's
#define USINGLEDSTRIP false

//if you want to change NUMPIXELMAPBYTES you will also have to change
//the relevant constant in the desktop application (also called NUMPIXELMAPBYTES)
#define NUMPIXELMAPBYTES 6
#define BESTECHNUMPIXELS 38
#define PIN        5
#define SERIALINPUTBUFFERSIZE 4096
#define MAXNUMLEDSTRIPS 5
#define MAXNUMFRAMES 128

struct Subframe
{
  byte pixelMap[NUMPIXELMAPBYTES] = {0};
  byte colorRed = 0;
  byte colorGreen = 0;
  byte colorBlue = 0;  
};

struct PixelColor
{
  byte colorRed = 0;
  byte colorGreen = 0;
  byte colorBlue = 0;
};

struct Frame
{
  unsigned int subframeCount = 0;
  unsigned int frameDuration = 0; //milliseconds
  byte ledstripID = 0;
  Subframe subframes[16];
};


Adafruit_NeoPixel* pixels[MAXNUMLEDSTRIPS];


Frame frames[MAXNUMLEDSTRIPS][MAXNUMFRAMES];
int frameCount[MAXNUMLEDSTRIPS] = {0};
int numPixels[MAXNUMLEDSTRIPS] = {0};
int ledstripPins[MAXNUMLEDSTRIPS]={0};

//circular buffer
byte serialInputBuffer[SERIALINPUTBUFFERSIZE]={0};

long serialInputBufferReadIndex = 0;
long serialInputBufferWriteIndex = 0;
long timeOfLastSerialRead;
long bytesProcessed=0;
long bytesWritten=0;

bool isClearPending = false;
bool isAnimationPending = false;
bool isAnimationPlaying = false;
int currentAnimationFrame = 0;
long frameTimes[MAXNUMLEDSTRIPS][MAXNUMFRAMES] = {0};

bool isAnimationRunningLoop = false;

PixelColor pixelColors[MAXNUMLEDSTRIPS][BESTECHNUMPIXELS];

void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200);
  numPixels[0]=BESTECHNUMPIXELS;
  ledstripPins[0]= 5;

  for(int i=0; i<MAXNUMLEDSTRIPS; i++)
  {
    if(ledstripPins[i] > 0)
    {
      pixels[i]=new Adafruit_NeoPixel(numPixels[i], ledstripPins[i], NEO_GRB + NEO_KHZ800);
      pixels[i]->begin();
    }
  }
  
  FlashLEDS();
  clearPixels();
}

void loop() {
  
  //read serial in as quickly as possible
  while(Serial.available())
  {
    char inByte = Serial.read();

    serialInputBuffer[serialInputBufferWriteIndex++] = inByte;

    if(serialInputBufferWriteIndex >= SERIALINPUTBUFFERSIZE)
      serialInputBufferWriteIndex = 0;

    bytesWritten++;
    timeOfLastSerialRead = millis();

    
  }

  //prevent overflow
    if(bytesWritten > 2147483640)
      bytesWritten = 0;
    if(bytesProcessed > 2147483640)
      bytesProcessed = 0;
      

  //this is where we process data...if there are at least 4 bytes to read
  if(abs(serialInputBufferReadIndex - serialInputBufferWriteIndex) >= 4 || (bytesWritten-bytesProcessed>=4))
  {
    //arbitrary wait for all serial data to come in
      if(millis() - timeOfLastSerialRead > 2)
      {
        if(serialInputBuffer[serialInputBufferReadIndex] == 0xAB && serialInputBuffer[(serialInputBufferReadIndex+1) % SERIALINPUTBUFFERSIZE] == 0xCD && serialInputBuffer[(serialInputBufferReadIndex+2) % SERIALINPUTBUFFERSIZE] == 0xEF)
        {
          //this is a frame
          byte stripID = serialInputBuffer[(serialInputBufferReadIndex+3) % SERIALINPUTBUFFERSIZE];
          int bytesRead = getFrameFromBytes(frames[stripID][frameCount[stripID]], serialInputBuffer, serialInputBufferReadIndex + 4);
          frameCount[stripID]++;
          serialInputBufferReadIndex = (serialInputBufferReadIndex + 4 + bytesRead) % SERIALINPUTBUFFERSIZE;

          bytesProcessed += 4 + bytesRead;
        }
        else if(serialInputBuffer[serialInputBufferReadIndex] == 0xBA && serialInputBuffer[(serialInputBufferReadIndex+1) % SERIALINPUTBUFFERSIZE] == 0xDC && serialInputBuffer[(serialInputBufferReadIndex+2) % SERIALINPUTBUFFERSIZE] == 0xFE)
        {
          //this is an led command
          //byte stripID = serialInputBuffer[(serialInputBufferReadIndex+3) % SERIALINPUTBUFFERSIZE];
           
          if(serialInputBuffer[(serialInputBufferReadIndex+3) % SERIALINPUTBUFFERSIZE] == 0xAA)
          {
            //clear frame buffer
            for(int i=0; i<MAXNUMLEDSTRIPS; i++)
              frameCount[i] = 0;
          }
          else if(serialInputBuffer[(serialInputBufferReadIndex+3) % SERIALINPUTBUFFERSIZE] == 0xBB)
          {
            //play animation
            isAnimationPending = true;
            isAnimationRunningLoop = false;
          }
          else if(serialInputBuffer[(serialInputBufferReadIndex+3) % SERIALINPUTBUFFERSIZE] == 0xCC)
          {
            //cancel currently playing animation
            for(int i=0; i<MAXNUMLEDSTRIPS; i++)
              frameCount[i] = 0;
              
            isAnimationPlaying = false;
            isAnimationRunningLoop = false;
            isClearPending = true;
          }
          else if(serialInputBuffer[(serialInputBufferReadIndex+3) % SERIALINPUTBUFFERSIZE] == 0xEE)
          {
            //play animation on loop
            isAnimationPending = true;
            isAnimationRunningLoop = true;
          }
          else
          {
            //undefined command
            Serial.println("UNDEFINED COMMAND!");
          }
          bytesProcessed+=4;
          serialInputBufferReadIndex = (serialInputBufferReadIndex + 4) % SERIALINPUTBUFFERSIZE;
          
        }
        else
        {
          //this is undefined. send an error message
          String errorMessage = "";
          errorMessage += serialInputBufferReadIndex;
          errorMessage += ".";
          errorMessage += serialInputBufferWriteIndex;
          errorMessage += ".";
          errorMessage += bytesWritten;
          errorMessage += ".";
          errorMessage += bytesProcessed;
          Serial.println(errorMessage);
          serialInputBufferReadIndex = 0;
          serialInputBufferWriteIndex = 0;
          bytesProcessed=0;
          bytesWritten=0;
          for(int i=0; i<MAXNUMLEDSTRIPS; i++)
              frameCount[i] = 0;
        }
      }
  }

  if(isClearPending)
  {
    isClearPending = false;
    clearPixels();
  }
  
  if(isAnimationPending)
  {
    isAnimationPending = false;
    isAnimationPlaying = true;
    currentAnimationFrame = 0;
    
    for(int a=0; a<MAXNUMLEDSTRIPS; a++) 
    {
      if(ledstripPins[a] > 0)
      {
        frameTimes[a][0]=millis();
        for(int i=1; i<frameCount[a]; i++)
        {
          frameTimes[a][i] = frameTimes[a][i-1] + frames[a][i].frameDuration;   
        }
  
        if(frameCount[a]>0)
          ApplyFrame(frames[a][0], (byte) a);
      }
    }
  }

  if(isAnimationPlaying)
  {
    for(int i=0; i<MAXNUMLEDSTRIPS; i++)
    {
      if(ledstripPins[i] > 0)
      {
        if(frameCount[i] >= currentAnimationFrame)
        {
          if(millis() > frameTimes[i][currentAnimationFrame] + frames[i][currentAnimationFrame].frameDuration)
          {
            if(currentAnimationFrame >= frameCount[i])
            {
              //animation complete
              if(isAnimationRunningLoop)
              {
                isAnimationPending = true;
                isAnimationPlaying = false;
              }
              else
              {
                //end animation
                isAnimationPlaying = false;
                frameCount[i] = 0;
                clearPixels();
              }
            }
            else
            {
              currentAnimationFrame++;
              ApplyFrame(frames[i][currentAnimationFrame], i);
            }
            
          }
        }
      }
    }
  }
}


void FlashLEDS()
{
  for(int a=0; a<MAXNUMLEDSTRIPS; a++)
  {
    if(ledstripPins[a] != 0)
    {
      for(int i=0; i<numPixels[a]; i++)
      {
        pixels[a]->setPixelColor(i, pixels[a]->Color(255,255,255));
        pixels[a]->show();
      }
      delay(2000);
      for(int i=0; i<numPixels[a]; i++)
      {
        pixels[a]->setPixelColor(i, pixels[a]->Color(0,0,0));
        pixels[a]->show();
      }
      delay(2000);
    }
  }
}


void ApplyFrame(Frame& frame, byte stripID)
{
  if(frame.subframeCount < 1)
    return;
    

  for(int i=0; i<frame.subframeCount; i++)
  {
  
    byte colorR = frame.subframes[i].colorRed;
    byte colorG = frame.subframes[i].colorGreen;
    byte colorB = frame.subframes[i].colorBlue;
    
  
    for(int a=0; a<NUMPIXELMAPBYTES; a++)
    {
      for(int b=0; b<8; b++)
      {
        if( ((frame.subframes[i].pixelMap[a] >> b) & 1) == 1)
        {
          //check to see if this pixel is already the correct color to cut down on unnecessary data transfers
          if(pixelColors[stripID][a*8+b].colorRed != colorR || pixelColors[stripID][a*8+b].colorGreen != colorG || pixelColors[stripID][a*8+b].colorBlue != colorB )
          {
            if(USINGLEDSTRIP)
              pixels[stripID]->setPixelColor(a*8+b, pixels[stripID]->Color(colorR, colorB, colorG));
            else
              pixels[stripID]->setPixelColor(a*8+b, pixels[stripID]->Color(colorR, colorG, colorB));
            
            pixelColors[stripID][a*8+b].colorRed=colorR;
            pixelColors[stripID][a*8+b].colorGreen=colorG;
            pixelColors[stripID][a*8+b].colorBlue=colorB;
          }
        }
      }
    }
  }
  
  pixels[stripID]->show();
}

void clearPixels()
{
  for(int a=0; a<MAXNUMLEDSTRIPS; a++)
  {
    if(ledstripPins[a] != 0)
    {
      for(int i=0; i<numPixels[a]; i++)
      {
        pixels[a]->setPixelColor(i, pixels[a]->Color(0,0,0));
        pixelColors[a][i].colorRed=0;
        pixelColors[a][i].colorGreen=0;
        pixelColors[a][i].colorBlue=0;
      }
      pixels[a]->show();
    }
  }
}

//returns the number of bytes read
int getFrameFromBytes(Frame& newFrame, byte byteArray[], int offset)
{
  newFrame.subframeCount = 0;
  byte numSubframes = byteArray[offset % SERIALINPUTBUFFERSIZE];

  
  newFrame.subframeCount = numSubframes;
  newFrame.frameDuration = byteArray[(offset + 1) % SERIALINPUTBUFFERSIZE] + (byteArray[(offset + 2) % SERIALINPUTBUFFERSIZE]<<8);

  int bytesPerSubframe = NUMPIXELMAPBYTES + 3;

  for(int j=0; j<numSubframes; j++)
  {

    newFrame.subframes[j].colorRed = byteArray[(j*bytesPerSubframe + 3 + offset) % SERIALINPUTBUFFERSIZE];
    newFrame.subframes[j].colorGreen = byteArray[(j*bytesPerSubframe + 4 + offset) % SERIALINPUTBUFFERSIZE];
    newFrame.subframes[j].colorBlue = byteArray[(j*bytesPerSubframe + 5 + offset) % SERIALINPUTBUFFERSIZE];

    for(int i=0; i<NUMPIXELMAPBYTES; i++)
    {
      newFrame.subframes[j].pixelMap[i] = byteArray[(j*bytesPerSubframe + 6 + i + offset) % SERIALINPUTBUFFERSIZE];
    }

  }

  int totalBytesRead = bytesPerSubframe * numSubframes + 3;

  return totalBytesRead; 
}
