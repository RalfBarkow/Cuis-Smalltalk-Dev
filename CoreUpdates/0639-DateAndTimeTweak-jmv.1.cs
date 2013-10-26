'From Cuis 2.9 of 5 November 2010 [latest update: #634] on 10 November 2010 at 2:25:13 pm'!!DateAndTime class methodsFor: 'ansi protocol' stamp: 'jmv 11/10/2010 14:25'!now 	| nanoTicks msm |	"No need to do this call, as #milliSecondsSinceMidnight will also do it"	"self waitForOffsets."		nanoTicks _ (msm _ self milliSecondsSinceMidnight) * 1000000.	(LastTick < nanoTicks) ifTrue: [		LastTick _ nanoTicks.		^ self todayAtMilliSeconds: msm].	LastTickSemaphore critical: [	 	 		LastTick _  LastTick + 1.		^ self todayAtNanoSeconds: LastTick]" [ 10000 timesRepeat: [ self now. ] ] timeToRun / 10000.0 . If calls to DateAndTime-c-#now are within a single millisecond the semaphore code to ensure that (self now <= self now) slows things down considerably by a factor of about 20.The actual speed of a single call to DateAndTime-now in milliseconds is demonstrated by the unguarded method below.[ 100000 timesRepeat: [ self todayAtMilliSeconds: (self milliSecondsSinceMidnight) ] ] timeToRun / 100000.0 .  0.00494 0.00481 0.00492 0.00495  "! !