#note: make sure to use python 3.8.6, because many libraries 
#(e.x. tensorflow) will not work with later versions
#https://www.python.org/ftp/python/3.8.6/python-3.8.6-amd64.exe

import cv2
import time
import numpy as np
import argparse
from imutils.video import FPS
import imutils

from fer import FER

BLUE = (255,0,0)
GREEN = (0,255,0)
RED = (0,0,255)


#fvs = FileVideoStream().start()
#time.sleep(1.0)

emotion_detector = FER(mtcnn=True, compile=True)

#from cv2 import cv2

#DeepFace.stream(db_path="C:\\User\\rriddel\\Desktop\\database", time_threshold=1, frame_threshold=10)
vidcap = cv2.VideoCapture(0)

prev_frame_time = 0
current_frame_time = 0

resize_and_bw_frames = True

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
    if ret is None:
        continue

    if resize_and_bw_frames:
        frame = imutils.resize(frame, width=450)
        frame = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
        frame = np.dstack([frame, frame, frame])
    

    result = emotion_detector.detect_emotions(frame)
    
    if len(result) == 0:
        print("No faces detected...")
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