#note: make sure to use python 3.8.6, because many libraries 
#(e.x. tensorflow) will not work with later versions
#https://www.python.org/ftp/python/3.8.6/python-3.8.6-amd64.exe

import cv2
import time
import os
from face_recognition.api import face_distance
import numpy as np
import argparse
from imutils.video import FPS
import imutils

from fer import FER

from deepface import DeepFace

import face_recognition

BLUE = (255,0,0)
GREEN = (0,255,0)
RED = (0,0,255)


#fvs = FileVideoStream().start()
#time.sleep(1.0)
resize_and_bw_frames = True
do_deepface = False
do_fer = False
do_mtcnn = False
do_face_detection = True
do_face_recognition = True
do_face_identification = True

emotion_detector = FER(mtcnn=do_mtcnn, compile=True)

#load previous face encodingz
known_face_encodings = []
known_face_names = []

people_db_size = 0
img_dir = "C:\\Users\\rriddel\\Desktop\\img\\people"
directory = os.fsencode(img_dir)
for file in os.listdir(directory):
    filename = os.fsdecode(file)
    pathname = img_dir + "\\" + filename
    person_name = filename.rsplit('.',1)[0]

    image_file = face_recognition.load_image_file(pathname)
    new_face_encoding = face_recognition.face_encodings(image_file)
    people_db_size = people_db_size + 1
    
    if len(new_face_encoding) > 0:
        known_face_encodings.append(new_face_encoding[0])
        known_face_names.append(person_name)



vidcap = cv2.VideoCapture(1)

prev_frame_time = 0
current_frame_time = 0


bounding_box = []
fps = FPS().start()

counter = 0
unknown_count=0

while True:

    #if fvs.more() == True:
    #    frame = fvs.read()
    #else:
    #    continue
    #current_frame_time = time.time()
    ##fps = 1/(current_frame_time - prev_frame_time)
    #prev_frame_time = current_frame_time
    #fps = int(fps)

    face_detected=False
    counter=counter+1

    ret, frame = vidcap.read()
    if ret is None:
        continue

    if resize_and_bw_frames:
        frame = imutils.resize(frame, width=450)
        #frame = imutils.resize(frame, width=256)
        frame = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
        frame = np.dstack([frame, frame, frame])
    
    #cv2.imwrite("C:\\Users\\rriddel\\Desktop\\img\\img.jpg", frame)

    if do_face_detection:
        #img = face_recognition.load_image_file("C:\\Users\\rriddel\\Desktop\\img\\img.jpg")
        img=frame
        face_locations = face_recognition.face_locations(img)
        face_landmarks = face_recognition.face_landmarks(img)
        if len(face_landmarks) != 0:
            
            face_detected=True

            if do_face_identification:
                xLoc = face_locations[0][0]
                if face_locations[0][0] < 10:
                    xLoc = 0
                else:
                    xLoc = face_locations[0][0]-10

                cropped_image = img[xLoc:face_locations[0][2], face_locations[0][3]:face_locations[0][1]]
                #cv2.imshow('frame', cropped_image)
                #cv2.imwrite("C:\\Users\\rriddel\\Desktop\\img\\unidentified.jpg", cropped_image)
                rgb_small_frame=img[:,:,::-1]

                

                face_encodings=face_recognition.face_encodings(rgb_small_frame, face_locations)
                matches = face_recognition.compare_faces(known_face_encodings, face_encodings[0])
                if len(matches) > 0:
                    name = "unknown"
                    face_distances = face_recognition.face_distance(known_face_encodings, face_encodings[0])

                    best_match_index = np.argmin(face_distances)
                    if matches[best_match_index]:
                        name = known_face_names[best_match_index]
                    print(name)
                    cv2.putText(frame, "ID " + name, (5,50), cv2.FONT_HERSHEY_SIMPLEX,0.5,RED,1,cv2.LINE_AA, )
                    if name == "unknown":
                        unknown_count = unknown_count + 1

                        if unknown_count > 2:
                            new_person_name = "person" + str(people_db_size) 
                            people_db_size = people_db_size + 1
                            cv2.imwrite("C:\\Users\\rriddel\\Desktop\\img\\people\\" + new_person_name + ".jpg", cropped_image)
                            known_face_encodings.append(face_encodings[0])
                            known_face_names.append(new_person_name)
                            unknown_count = 0
                    else:
                        unknown_count = 0

                cv2.rectangle(frame,(face_locations[0][3], face_locations[0][0]),(face_locations[0][1], face_locations[0][2]),(155, 155, 255), 2,)
                    


    if do_fer and face_detected and counter % 2 == 0:
        result = emotion_detector.detect_emotions(frame)
        
        if len(result) == 0:
            cv2.putText(frame, "No Faces Detected...", (5,25), cv2.FONT_HERSHEY_SIMPLEX,0.5,RED,1,cv2.LINE_AA, )
            cv2.imshow('frame', frame)
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
        
    
    #print(str(fvs.Q.qsize()))
    
    if do_deepface:
        resp = DeepFace.analyze(img_path="C:\\Users\\rriddel\\Desktop\\img\\img.jpg", actions = ['age','gender','race', 'emotion'], enforce_detection=False, prog_bar=False)
        
        
        if resp is not None:
            age_str = str(resp['age'])
            race_str = str(resp['dominant_race'])
            domemo_str = str(resp['dominant_emotion'])
            emotions2 = resp["emotion"]

            deetString = "Age: " + age_str + ", Race: " + race_str 
            deetString2 = "Emotion: " + domemo_str

            xLoc = resp["region"]["x"]
            yLoc = resp["region"]["y"]
            wLoc = resp["region"]["w"]
            hLoc = resp["region"]["h"]

            cv2.rectangle(frame,(xLoc, yLoc),(xLoc+wLoc, yLoc+hLoc),(0, 155, 255), 2,)

            cv2.putText(frame, deetString, (xLoc-30, yLoc-25), cv2.FONT_HERSHEY_SIMPLEX, 0.5, BLUE, 1, cv2.LINE_AA,)
            cv2.putText(frame, deetString2, (xLoc-30, yLoc-10), cv2.FONT_HERSHEY_SIMPLEX, 0.5, BLUE, 1, cv2.LINE_AA,)

            cv2.putText(frame, "Fear: " + str(int(emotions2["fear"])), (xLoc-100, yLoc), cv2.FONT_HERSHEY_SIMPLEX, 0.5, BLUE, 1, cv2.LINE_AA,)
            cv2.putText(frame, "Sad: " + str(int(emotions2["sad"])), (xLoc-100, yLoc+15), cv2.FONT_HERSHEY_SIMPLEX, 0.5, BLUE, 1, cv2.LINE_AA,)
            cv2.putText(frame, "Angry: " + str(int(emotions2["angry"])), (xLoc-100, yLoc+30), cv2.FONT_HERSHEY_SIMPLEX, 0.5, BLUE, 1, cv2.LINE_AA,)
            cv2.putText(frame, "Neutral: " + str(int(emotions2["neutral"])), (xLoc-100, yLoc+45), cv2.FONT_HERSHEY_SIMPLEX, 0.5, BLUE, 1, cv2.LINE_AA,)
            cv2.putText(frame, "Happy: " + str(int(emotions2["happy"])), (xLoc-100, yLoc+60), cv2.FONT_HERSHEY_SIMPLEX, 0.5, BLUE, 1, cv2.LINE_AA,)
            cv2.putText(frame, "Surprise: " + str(int(emotions2["surprise"])), (xLoc-100, yLoc+75), cv2.FONT_HERSHEY_SIMPLEX, 0.5, BLUE, 1, cv2.LINE_AA,)

            
    
    
    fps.update()
    fps.stop()
    print("[INFO] approx. FPS: {:.2f}".format(fps.fps()))
    #fps = vidcap.get(cv2.CAP_PROP_FPS)
    
    cv2.putText(frame, str(round(fps.fps(),2)) + "fps", (5,25), cv2.FONT_HERSHEY_SIMPLEX,0.5,RED,1,cv2.LINE_AA, )
    #print(fps)
    
    cv2.imshow('frame', frame)
    
    
    
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

vidcap.release()
cv2.destroyAllWindows()