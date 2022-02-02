This readme pertains to lightstudio only.  This repo needs to be cleaned up.

Serial Protocol


				frame message
[-----header-----][---------framedata-----------]
[0xab][0xcd][0xef][#subframes][duration][sframes]

0xab = 171
0xcd = 205
0xef = 239


	   command message
[-----header-----][--command--]
[0xba][0xdc][0xfe][command byte]

commands:
0xaa=170=clear frame buffer
0xbb=187=play animation
0xcc=204=
0xdd=221=
0xee=238=reserved


0xba=186
0xdc=220
0xfe=254


responses
0xfa = OK
0xeb = Resend last message
