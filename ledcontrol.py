import time
import RPi.GPIO as GPIO
import spidev

import board
import neopixel
num_pixels = 105
pixel_pin = board.D18
ORDER = neopixel.GRB
pixels = neopixel.NeoPixel(pixel_pin, num_pixels, brightness=0.9, auto_write=False, pixel_order = ORDER)

#SPI init
spi_bus_num = 0
spi_device_num = 1
spi_bus = spidev.SpiDev()
spi_bus.open(spi_bus_num, spi_device_num)
spi_bus.max_speed_hz = 500000
spi_bus.mode = 0
#END SPI init

#I2C init
import smbus
i2c_channel = 1
i2c_device_address = 0x60
i2c_reg_write = 0x40
i2c_bus = smbus.SMBus(i2c_channel)
#END I2C init

print("Beginning LED Controller...")
ledpin = 23
GPIO.setwarnings(False)
GPIO.setmode(GPIO.BCM)
GPIO.setup(ledpin, GPIO.OUT)
GPIO.output(ledpin, GPIO.HIGH)


def wheel(pos):
    # Input a value 0 to 255 to get a color value.
    # The colours are a transition r - g - b - back to r.
    if pos < 0 or pos > 255:
        r = g = b = 0
    elif pos < 85:
        r = int(pos * 3)
        g = int(255 - pos * 3)
        b = 0
    elif pos < 170:
        pos -= 85
        r = int(255 - pos * 3)
        g = 0
        b = int(pos * 3)
    else:
        pos -= 170
        r = 0
        g = int(pos * 3)
        b = int(255 - pos * 3)
    return (r, g, b) if ORDER in (neopixel.RGB, neopixel.GRB) else (r, g, b, 0)


def rainbow_cycle(wait):
    for j in range(255):
        for i in range(num_pixels):
            pixel_index = (i * 256 // num_pixels) + j
            pixels[i] = wheel(pixel_index & 255)
        pixels.show()
        time.sleep(wait)

def experimental(wait):
    for j in range(55):
        for i in range(num_pixels):
            pixels[i] = (255,50,50)
            if i < num_pixels-2:
                pixels[i+1] = (50,255,50)

            if i > 0:
                pixels[i-1]=(0,0,0)
            pixels.show()



try:
    while True:
        GPIO.output(ledpin, GPIO.LOW)

        rainbow_cycle(0.001)  # rainbow cycle with 1ms delay per step

        GPIO.output(ledpin, GPIO.HIGH)

        rainbow_cycle(0.001)

except KeyboardInterrupt:
    for i in range(num_pixels):
        pixels[i]=(0,0,0)
        pixels.show()
    GPIO.cleanup()