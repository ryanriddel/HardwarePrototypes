from heartrate_monitor import HeartRateMonitor
import time
import statistics

hrm = HeartRateMonitor()
hrm.start_sensor()
counter=0
sample_list = []

while True:
	beats = hrm.bpm
	if beats != 0:
		print(beats)
		counter = counter + 1
		sample_list.append(beats)
		
		if counter % 25 == 0:
			stddev = round(statistics.stdev(sample_list), 2)
			mean = int(sum(sample_list)/len(sample_list))
			print("StdDev: " + str(stddev) + "   Mean: " + str(mean)
			
	time.sleep(2)
		
hrm.stop_sensor()