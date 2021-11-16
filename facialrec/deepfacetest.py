#note: make sure to use python 3.8.6, because many libraries 
#(e.x. tensorflow) will not work with later versions
#https://www.python.org/ftp/python/3.8.6/python-3.8.6-amd64.exe


from deepface import DeepFace


DeepFace.stream(db_path="C:\\User\\rriddel\\Desktop\\database", model_name="Facenet", time_threshold=1, frame_threshold=4)
#vidcap = cv2.VideoCapture(0)
BLUE = (255,0,0)
GREEN = (0,255,0)
RED = (0,0,255)

prev_frame_time = 0
current_frame_time = 0

resize_and_bw_frames = False
