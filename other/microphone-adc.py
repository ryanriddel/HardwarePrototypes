import busio
import digitalio
import board
import time
import adafruit_mcp3xxx.mcp3008 as MCP
from adafruit_mcp3xxx.analog_in import AnalogIn

spi = busio.SPI(clock=board.SCK, MISO=board.MISO, MOSI=board.MOSI)
cs = digitalio.DigitalInOut(board.D5)
mcp = MCP.MCP3008(spi, cs)

channel0 = AnalogIn(mcp, MCP.P0)
channel1 = AnalogIn(mcp, MCP.P1)
channel2 = AnalogIn(mcp, MCP.P2)
channel3 = AnalogIn(mcp, MCP.P3)

counter=0
start = time.time()

while True:
    #time.sleep(0.01)
    #print((channel0.value, channel1.value))
    val1 = channel0.value
    val2 = channel1.value
    #print(str(channel2.value) + "   " + str(channel3.value))
    counter = counter + 1

    if counter%100 == 0:
        end = time.time()
        interval = end-start
        print(str(int(100/interval)))
        start=time.time()