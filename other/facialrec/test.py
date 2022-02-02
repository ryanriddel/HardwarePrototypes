#note: make sure to use python 3.8.6, because many libraries 
#(e.x. tensorflow) will not work with later versions
#https://www.python.org/ftp/python/3.8.6/python-3.8.6-amd64.exe

import cv2
import time
import numpy as np
import argparse
from imutils.video import FPS
import imutils
from retinaface import RetinaFace
from deepface import DeepFace
from fer import FER
import matplotlib.pyplot as plt
import matplotlib.image as mpimg

from threading import Thread
import sys
from queue import Queue


class FileVideoStream:
    def __init__(self, queueSize=1024):
		# initialize the file video stream along with the boolean
		# used to indicate if the thread should be stopped or not
        self.stream = cv2.VideoCapture(0)
        self.stopped = False
		# initialize the queue used to store frames read from
		# the video file
        self.Q = Queue(maxsize = queueSize)

    def start(self):
        t = Thread(target=self.update, args=())
        t.daemon = True
        t.start()
        return self

    def update(self):
		# keep looping infinitely
        while True:
			# if the thread indicator variable is set, stop the
			# thread
            if self.stopped:
                return
			# otherwise, ensure the queue has room in it
            if not self.Q.full():
				# read the next frame from the file
                (grabbed, frame) = self.stream.read()
				# if the `grabbed` boolean is `False`, then we have
				# reached the end of the video file
                if not grabbed:
                    self.stop()
                    return
				# add the frame to the queue
                
                self.Q.put(frame)

    def read(self):
        return self.Q.get()

    def more(self):
        return self.Q.qsize() > 0

    def stop(self):
        self.stopped = True



#fvs = FileVideoStream().start()
#time.sleep(1.0)

emotion_detector = FER(mtcnn=False)

#from cv2 import cv2

#DeepFace.stream(db_path="C:\\User\\rriddel\\Desktop\\database", time_threshold=1, frame_threshold=10)
vidcap = cv2.VideoCapture(0)
BLUE = (255,0,0)
GREEN = (0,255,0)
RED = (0,0,255)

prev_frame_time = 0
current_frame_time = 0

resize_and_bw_frames = False

fps = FPS().start()


while True:

    #if fvs.more() == True:
    #    frame = fvs.read()
    #else:
    #    continue
    #current_frame_time = time.time()
    ##fps = 1/(current_frame_time - prev_frame_time)
    #prev_frame_time = current_frame_time
    #fps = int(fps)
    ret, frame = vidcap.read()
    if resize_and_bw_frames:
        frame = imutils.resize(frame, width=450)
        frame = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
        frame = np.dstack([frame, frame, frame])
    
    #cv2.imwrite("C:\\Users\\rriddel\\Desktop\\img.jpg", frame)
    #print(str(fvs.Q.qsize()))
    #resp = RetinaFace.detect_faces("C:\\Users\\rriddel\\Desktop\\img.jpg")
    #p0 = resp['face_1']['facial_area'][0], resp['face_1']['facial_area'][1]
    #p1 = resp['face_1']['facial_area'][2], resp['face_1']['facial_area'][3]
    #cv2.rectangle(frame, p0, p1, BLUE, 2)
    #nosePoint1 = int(resp['face_1']['landmarks']['nose'][0])
    #nosePoint2 = int(resp['face_1']['landmarks']['nose'][1])
    #cv2.circle(frame, (int(nosePoint1), int(nosePoint2)), 5, BLUE, 1)
    #rightEyeX = resp['face_1']['landmarks']['right_eye'][0] 
    #rightEyeY = resp['face_1']['landmarks']['right_eye'][1]
    ##leftEyeX = resp['face_1']['landmarks']['left_eye'][0]
    #leftEyeY = resp['face_1']['landmarks']['left_eye'][1]
    #cv2.circle(frame, (int(rightEyeX), int(rightEyeY)), 5, BLUE)
    #cv2.circle(frame, (int(leftEyeX), int(leftEyeY)), 5, BLUE)
    result = emotion_detector.detect_emotions(frame)
    
    if len(result) == 0:
        print("No faces detected")
        continue
    
    bounding_box = result[0]["box"]
    emotions = result[0]["emotions"]
    cv2.rectangle(frame,(
    bounding_box[0], bounding_box[1]),(
    bounding_box[0] + bounding_box[2], bounding_box[1] + bounding_box[3]),
                (0, 155, 255), 2,)

    emotion_name, score = emotion_detector.top_emotion(frame )
    for index, (emotion_name, score) in enumerate(emotions.items()):
        color = (211, 211,211) if score < 0.1 else (0, 0, 255)
        emotion_score = "{}: {}".format(emotion_name, "{:.2f}".format(score))
    
        cv2.putText(frame,emotion_score, (bounding_box[0], bounding_box[1] + bounding_box[3] + 30 + index * 15), cv2.FONT_HERSHEY_SIMPLEX,0.5,color,1,cv2.LINE_AA,)
    
    
    fps.update()
    fps.stop()
    print("[INFO] approx. FPS: {:.2f}".format(fps.fps()))
    #fps = vidcap.get(cv2.CAP_PROP_FPS)
    
    cv2.putText(frame, str(fps.fps()) + "fps", (5,25), cv2.FONT_HERSHEY_SIMPLEX,0.5,RED,1,cv2.LINE_AA, )
    #print(fps)
    
    cv2.imshow('frame', frame)
    
    
    
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

#vidcap.release()
cv2.destroyAllWindows()