'From Cuis 2.3 of 22 March 2010 [latest update: #472] on 9 April 2010 at 2:25:57 pm'!!DateAndTime methodsFor: 'ansi protocol' stamp: 'jmv 4/9/2010 13:22'!+ operand	"operand conforms to protocol Duration"	| ticks |	self assert: operand class == Duration. 	ticks _ self ticks + (operand ticks).	^ self class basicNew		ticks: ticks		offset: self offset; 		yourself! !!DateAndTime methodsFor: 'ansi protocol' stamp: 'jmv 4/9/2010 13:22'!- operand	"operand is a DateAndTime or a Duration"	operand class == DateAndTime		ifTrue: [			| lticks rticks |			lticks _ self asLocal ticks.			rticks _ operand asLocal ticks.			^Duration 				seconds: (SecondsInDay *(lticks first - rticks first)) + 							(lticks second - rticks second) 				nanoSeconds: (lticks third - rticks third) ].		^self + operand negated! !!DateAndTime methodsFor: 'ansi protocol' stamp: 'jmv 4/9/2010 13:18'!< comparand	"comparand conforms to protocol DateAndTime,	or can be converted into something that conforms."	| lticks rticks |	self assert: comparand class == DateAndTime.	offset = comparand offset		ifTrue: [lticks _ self ticks.			rticks _ comparand ticks]		ifFalse: [lticks _ self asUTC ticks.			rticks _ comparand asUTC ticks].	^ lticks first < rticks first		or: [lticks first > rticks first				ifTrue: [false]				ifFalse: [lticks second < rticks second						or: [lticks second > rticks second								ifTrue: [false]								ifFalse: [lticks third < rticks third]]]]! !!DateAndTime methodsFor: 'ansi protocol' stamp: 'jmv 4/9/2010 13:16'!= comparand	"comparand conforms to protocol DateAndTime,	or can be converted into something that conforms."	| |	self == comparand		ifTrue: [^ true].	self class == comparand class ifFalse: [ ^false ].	^ self offset = comparand offset		ifTrue: [self hasEqualTicks: comparand ]		ifFalse: [self asUTC ticks = comparand asUTC ticks]! !!DateAndTime methodsFor: 'ansi protocol' stamp: 'jmv 4/9/2010 11:57'!isLeapYear	^ Year isLeapYear: self yearNumber! !!DateAndTime methodsFor: 'ansi protocol' stamp: 'jmv 4/9/2010 12:51'!meridianAbbreviation	^ self time meridianAbbreviation! !!DateAndTime methodsFor: 'ansi protocol' stamp: 'jmv 4/9/2010 11:50'!monthName	^ Month nameOfMonth: self monthIndex! !!DateAndTime methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 13:18'!to: anEnd	"Answer a Timespan. anEnd conforms to protocol DateAndTime or protocol Timespan"		self assert: anEnd class == DateAndTime.	^ Timespan starting: self ending: anEnd! !!DateAndTime methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 11:12'!to: anEnd by: aDuration	"Answer a Timespan. anEnd conforms to protocol DateAndTime or protocol Timespan"	self assert: anEnd class == DateAndTime.	self assert: aDuration class == Duration.	^ (Schedule starting: self ending: anEnd)		schedule: (Array with: aDuration);		yourself.! !!DateAndTime methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 11:12'!utcOffset: anOffset	"Answer a <DateAndTime> equivalent to the receiver but offset from UTC by anOffset"	| equiv |	self assert: anOffset class == Duration.	equiv _ self + (anOffset - self offset).	^ equiv ticks: equiv ticks offset: anOffset; yourself! !!DateAndTime methodsFor: 'smalltalk-80' stamp: 'jmv 4/9/2010 12:37'!daysInMonth	"Answer the number of days in the month represented by the receiver."	^ self month daysInMonth! !!DateAndTime methodsFor: 'smalltalk-80' stamp: 'jmv 4/9/2010 12:42'!daysInYear	"Answer the number of days in the year represented by the receiver."	^ (Year including: self) daysInYear! !!DateAndTime methodsFor: 'smalltalk-80' stamp: 'jmv 4/9/2010 12:37'!firstDayOfMonth	^ self month start dayOfYear! !!DateAndTime methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 11:34'!date	^Date including: self! !!DateAndTime methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 11:35'!month	^Month including: self! !!DateAndTime methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 11:56'!monthIndex	^ self dayMonthYearDo:		[ : d : m : y |  m ]! !!DateAndTime methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 13:37'!secondsSinceSqueakEpoch	"Return the number of seconds since the Squeak epoch"	^ (self - (self class epoch)) totalSeconds! !!DateAndTime methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 10:34'!time	^Time seconds: seconds nanoSeconds: nanos! !!DateAndTime methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 11:35'!week	^Week including: self! !!DateAndTime methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 11:35'!year	^Year including: self! !!DateAndTime methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 11:56'!yearNumber	^ self		dayMonthYearDo: [ :d :m :y | y ]! !!DateAndTime class methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 11:58'!date: aDate time: aTime	^ self 		year: aDate yearNumber 		day: aDate dayOfYear 		hour: aTime hour 		minute: aTime minute 		second: aTime second! !!DateAndTime class methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 11:58'!readFrom: aStream	| offset date time ch |	date _ Date readFrom: aStream.	[aStream peek isDigit]		whileFalse: [aStream next].	time _ Time readFrom: aStream.	aStream atEnd		ifTrue: [ offset _ self localOffset ]		ifFalse: [			ch _ aStream next.			ch = $+ ifTrue: [ch _ Character space].			offset _ Duration fromString: ch asString, '0:', aStream upToEnd, ':0'].			^ self		year: date yearNumber		month: date monthIndex		day: date dayOfMonth		hour: time hour		minute: time minute		second: time second		nanoSecond: time nanoSecond		offset: offset	"	'-1199-01-05T20:33:14.321-05:00' asDateAndTime		' 2002-05-16T17:20:45.1+01:01' asDateAndTime		' 2002-05-16T17:20:45.02+01:01' asDateAndTime		' 2002-05-16T17:20:45.003+01:01' asDateAndTime		' 2002-05-16T17:20:45.0004+01:01' asDateAndTime  		' 2002-05-16T17:20:45.00005' asDateAndTime		' 2002-05-16T17:20:45.000006+01:01' asDateAndTime		' 2002-05-16T17:20:45.0000007+01:01' asDateAndTime		' 2002-05-16T17:20:45.00000008-01:01' asDateAndTime   		' 2002-05-16T17:20:45.000000009+01:01' asDateAndTime  		' 2002-05-16T17:20:45.0000000001+01:01' asDateAndTime   		' 2002-05-16T17:20' asDateAndTime		' 2002-05-16T17:20:45' asDateAndTime		' 2002-05-16T17:20:45+01:57' asDateAndTime 		' 2002-05-16T17:20:45-02:34' asDateAndTime 		' 2002-05-16T17:20:45+00:00' asDateAndTime		' 1997-04-26T01:02:03+01:02:3' asDateAndTime  	"! !!DateAndTime class methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 12:44'!tomorrow	^ self today date next start! !!DateAndTime class methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 12:44'!yesterday	^ self today date previous start! !!DateAndTime class methodsFor: 'initialize-release' stamp: 'jmv 4/9/2010 13:41'!initializeOffsets	| epochianSeconds secondsSinceMidnight nowSecs  |	  	LastTick _ 0.  	nowSecs _  self clock secondsWhenClockTicks.	LastMilliSeconds _ self millisecondClockValue. 	epochianSeconds _ Duration days: SqueakEpoch hours: 0 minutes: 0 seconds: nowSecs.	DaysSinceEpoch _ epochianSeconds days.	secondsSinceMidnight _ (epochianSeconds - (Duration days: DaysSinceEpoch hours: 0 minutes: 0 seconds: 0)) totalSeconds.  	MilliSecondOffset _ (secondsSinceMidnight * 1000 - LastMilliSeconds)! !!DateAndTime class methodsFor: 'initialize-release' stamp: 'jmv 4/9/2010 13:40'!startUp: resuming 	resuming ifFalse: [^ self].	self initializeOffsets! !!Delay class methodsFor: 'instance creation' stamp: 'jmv 4/9/2010 14:09'!forDuration: aDuration	^ self forMilliseconds: aDuration totalMilliSeconds! !!Duration methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 14:09'!totalMilliSeconds	^ self totalNanoSeconds // NanosInMillisecond! !!Duration methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 14:07'!totalNanoSeconds	^ (seconds * NanosInSecond) + nanos! !!Duration methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 13:38'!totalSeconds	^ seconds! !!Duration methodsFor: 'ansi protocol' stamp: 'jmv 4/9/2010 14:07'!* operand	"operand is a Number" 	^ self class nanoSeconds: ( (self totalNanoSeconds * operand) asInteger).! !!Duration methodsFor: 'ansi protocol' stamp: 'jmv 4/9/2010 14:07'!+ operand	"operand is a Duration" 	^ self class nanoSeconds: (self totalNanoSeconds + operand totalNanoSeconds)! !!Duration methodsFor: 'ansi protocol' stamp: 'jmv 4/9/2010 14:08'!/ operand	"operand is a Duration or a Number"	^ operand isNumber		ifTrue: [ self class nanoSeconds: (self totalNanoSeconds / operand) asInteger ]		ifFalse: [			self assert: operand class == Duration.			self totalNanoSeconds / operand totalNanoSeconds ].! !!Duration methodsFor: 'ansi protocol' stamp: 'jmv 4/9/2010 14:07'!< comparand	^ self totalNanoSeconds < comparand totalNanoSeconds! !!Duration methodsFor: 'ansi protocol' stamp: 'jmv 4/9/2010 14:07'!= comparand 	"Answer whether the argument is a <Duration> representing the same 	period of time as the receiver."	^ self == comparand		ifTrue: [true]		ifFalse: 			[self species = comparand species 				ifTrue: [self totalNanoSeconds = comparand totalNanoSeconds]				ifFalse: [false] ]! !!Duration methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 14:08'!// operand	"operand is a Duration or a Number"	^ operand isNumber		ifTrue: [ self class nanoSeconds: (self totalNanoSeconds // operand) asInteger ]		ifFalse: [			self assert: operand class == Duration.			self totalNanoSeconds // operand totalNanoSeconds ]! !!Duration methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 14:07'!\\ operand	"modulo. Remainder defined in terms of //. Answer a Duration with the 	same sign as aDuration. operand is a Duration or a Number."	^ operand isNumber		ifTrue: [ self class nanoSeconds: (self totalNanoSeconds \\ operand) ]		ifFalse: [ self - (operand * (self // operand)) ]! !!Duration methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 14:07'!roundTo: aDuration	"e.g. if the receiver is 5 minutes, 37 seconds, and aDuration is 2 minutes, answer 6 minutes."	^ self class nanoSeconds: (self totalNanoSeconds roundTo: aDuration totalNanoSeconds)! !!Duration methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 14:08'!truncateTo: aDuration	"e.g. if the receiver is 5 minutes, 37 seconds, and aDuration is 2 minutes, answer 4 minutes."	^ self class		nanoSeconds: (self totalNanoSeconds truncateTo: aDuration totalNanoSeconds)! !!Duration class methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 14:24'!milliSeconds: milliCount	^ self nanoSeconds: milliCount * NanosInMillisecond! !!ScrollingLabel2LW methodsFor: 'stepping and presenter' stamp: 'jmv 4/9/2010 09:18'!step	(hideTime notNil and: [		DateAndTime now > hideTime ])			ifTrue: [				^self hide ].	scrollPos _ scrollPos + 1.	scrollPos > contents size ifTrue: [		scrollPos _ 1 ].	self comeToFront.	self changed! !!ScrollingLabel2LW methodsFor: 'accessing' stamp: 'jmv 4/9/2010 09:18'!hideIn: seconds	hideTime _ DateAndTime now + seconds seconds! !!ScrollingLabelLW methodsFor: 'stepping and presenter' stamp: 'jmv 4/9/2010 09:19'!step	(hideTime notNil and: [		DateAndTime now > hideTime ])			ifTrue: [				^self hide ].	scrollPos _ scrollPos + 1.	scrollPos > labelWidth ifTrue: [		self initializeScroll].	self comeToFront.	self changed! !!ScrollingLabelLW methodsFor: 'accessing' stamp: 'jmv 4/9/2010 09:19'!hideIn: seconds	hideTime _ DateAndTime now + seconds seconds! !!TestRunner methodsFor: 'accessing' stamp: 'jmv 4/9/2010 13:30'!timeSinceLastPassAsString: aResult        (lastPass isNil or: [aResult hasPassed not]) ifTrue: [^ ''].        ^ ', ' , (DateAndTime now - lastPass) printString , 'since last Pass'! !!TestRunner methodsFor: 'updating' stamp: 'jmv 4/9/2010 13:29'!updateDetails: aTestResult 	self displayDetails: aTestResult printString			, (self timeSinceLastPassAsString: aTestResult).	aTestResult hasPassed		ifTrue: [lastPass _ DateAndTime now]! !!Time methodsFor: 'ansi protocol' stamp: 'jmv 4/9/2010 10:10'!< aTime	^seconds < aTime privateSeconds or: [ 		seconds = aTime privateSeconds and: [ nanos < aTime privateNanos ]]! !!Time methodsFor: 'ansi protocol' stamp: 'jmv 4/9/2010 10:12'!hour24	^ self asDurationSinceMidnight hours! !!Time methodsFor: 'ansi protocol' stamp: 'jmv 4/9/2010 10:12'!minute	^ self asDurationSinceMidnight minutes! !!Time methodsFor: 'ansi protocol' stamp: 'jmv 4/9/2010 10:12'!second	^ self asDurationSinceMidnight seconds! !!Time methodsFor: 'printing' stamp: 'jmv 4/9/2010 14:00'!printOn: aStream 	self print24: false		showSeconds: (self second ~= 0				or: [self nanoSecond ~= 0])		on: aStream! !!Time methodsFor: 'private' stamp: 'jmv 4/9/2010 10:09'!privateNanos	^nanos! !!Time methodsFor: 'private' stamp: 'jmv 4/9/2010 10:09'!privateSeconds	^seconds! !!Time methodsFor: 'private' stamp: 'jmv 4/9/2010 10:08'!seconds: secondCount nanoSeconds: nanoCount 	"Private - only used by Time class."	self assert: nanoCount < NanosInSecond.	seconds _ secondCount.	nanos _ nanoCount! !!Time methodsFor: 'private' stamp: 'jmv 4/9/2010 10:08'!ticks: anArray	"ticks is an Array: { days. seconds. nanoSeconds }"	seconds _ anArray at: 2.	nanos _ anArray at: 3.	self assert: nanos < NanosInSecond.! !!Time methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 13:01'!- aTime	^ self asDurationSinceMidnight - aTime asDurationSinceMidnight! !!Time methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 10:12'!asDurationSinceMidnight	"Answer the duration since midnight"	^ Duration seconds: seconds nanoSeconds: nanos! !!Time class methodsFor: 'general inquiries' stamp: 'jmv 4/9/2010 11:58'!humanWordsForSecondsAgo: secs	| date today |	"Return natural language for this date and time in the past."	secs <= 1 ifTrue: [^ 'a second ago'].	secs < 45 ifTrue: [^ secs printString, ' seconds ago'].	secs < 90 ifTrue: [^ 'a minute ago'].	secs < "45*60" 2700 ifTrue: [^ (secs//60) printString, ' minutes ago'].	secs < "90*60" 5400 ifTrue: [^ 'an hour ago'].	secs < "18*60*60" 64800 ifTrue: [^ (secs//3600) printString, ' hours ago'].	date _ Date fromSeconds: self totalSeconds - secs.		"now work with dates"	today _ Date today.	date > (today - 2 days) ifTrue: [^ 'yesterday'].	date > (today - 8 days) ifTrue: [^ 'last ', date dayOfWeekName].	date > (today - 13 days) ifTrue: [^ 'a week ago'].	date > (today - 28 days) ifTrue: [		^ ((today - date) days //7) printString, ' weeks ago'].	date > (today - 45 days) ifTrue: [^ 'a month ago'].	date > (today - 300 days) ifTrue: [^ 'last ', date monthName].	^ date monthName, ', ', date yearNumber printString"Example#(0.5 30 62 130 4000 10000 60000 90000 345600 864000 1728000 3456000 17280000 34560000 345600000) 		collect: [:ss | Time humanWordsForSecondsAgo: ss]."! !!Timespan methodsFor: 'ansi protocol' stamp: 'jmv 4/9/2010 13:05'!+ aDuration	^ self class starting: self start + aDuration duration: self duration! !!Timespan methodsFor: 'ansi protocol' stamp: 'jmv 4/9/2010 13:05'!- aDuration	^ self class starting: self start - aDuration duration: self duration! !!Timespan methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 14:08'!end	^ self duration totalNanoSeconds = 0		ifTrue: [ self start ]		ifFalse: [ self next start - DateAndTime clockPrecision ]! !!Timespan methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 13:07'!includes: aDateAndTime	^ (aDateAndTime isKindOf: Timespan)			ifTrue: [ (self includes: aDateAndTime start)						and: [ self includes: aDateAndTime end ] ]			ifFalse: [ aDateAndTime between: start and: self end ]! !!Timespan methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 13:13'!start: aDateAndTime	"Store the start DateAndTime of this timespan"	self assert: aDateAndTime class == DateAndTime.	start _ aDateAndTime! !!Timespan methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 12:55'!to: anEnd	"Answer an Timespan. anEnd must be aDateAndTime"	self assert: anEnd class == DateAndTime.	^ Timespan starting: self start ending: anEnd! !!Timespan methodsFor: 'enumerating' stamp: 'jmv 4/9/2010 12:49'!workDatesDo: aBlock	"Exclude Saturday and Sunday"	self do: aBlock with: start date when: [ :d | d dayOfWeek < 6 ].! !!Timespan methodsFor: 'smalltalk-80' stamp: 'jmv 4/9/2010 13:11'!next	^self class classDefinesDuration		ifTrue: [ self class including: start + duration ]		ifFalse: [ self class starting: start + duration duration: duration ]! !!Timespan methodsFor: 'smalltalk-80' stamp: 'jmv 4/9/2010 13:10'!previous	^self class classDefinesDuration		ifTrue: [ self class including: start - duration ]		ifFalse: [ self class starting: start - duration duration: duration ]! !!Date methodsFor: 'smalltalk-80' stamp: 'jmv 4/9/2010 09:44'!previous: dayName 	"Answer the previous date whose weekday name is dayName."	| n |	n _ 7 + self weekdayIndex - (self class dayOfWeek: dayName) \\ 7.	n = 0 ifTrue: [ n _ 7 ].	^ self - n days! !!Date methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 12:08'!dayOfMonth	"Answer the day of the month represented by the receiver."	^ start dayOfMonth! !!Date methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 12:10'!dayOfWeek	"Answer the day of the week represented by the receiver."	^ start dayOfWeek! !!Date methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 12:08'!dayOfWeekName	"Answer the day of the week represented by the receiver."	^ start dayOfWeekName! !!Date methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 12:07'!dayOfYear	"Answer the day of the year represented by the receiver."	^ start dayOfYear! !!Date methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 12:10'!daysInMonth	^ start daysInMonth! !!Date methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 12:10'!daysInYear	"Answer the number of days in the month represented by the receiver."	^ start daysInYear! !!Date methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 12:20'!daysLeftInYear	^ start daysLeftInYear! !!Date methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 12:12'!firstDayOfMonth	^ start firstDayOfMonth! !!Date methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 12:09'!isLeapYear	^ start isLeapYear! !!Date methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 12:13'!julianDayNumber	^ start julianDayNumber! !!Date methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 12:31'!month	^start month! !!Date methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 12:15'!monthAbbreviation	^ start monthAbbreviation! !!Date methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 12:17'!monthIndex	^ start monthIndex! !!Date methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 12:18'!monthName	^ start monthName! !!Date methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 13:37'!secondsSinceSqueakEpoch	"Answer the seconds since the Squeak epoch: 1 January 1901"	^ start secondsSinceSqueakEpoch! !!Date methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 12:32'!week	^start week! !!Date methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 12:32'!year	^start year! !!Date methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 12:20'!yearNumber	^ start yearNumber! !!Month methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 13:45'!daysInMonth	^ self duration days! !!Month methodsFor: 'printing' stamp: 'jmv 4/9/2010 11:58'!printOn: aStream	aStream nextPutAll: self monthName, ' ', self yearNumber printString! !!Month methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 12:11'!daysInYear	"Answer the number of days in the month represented by the receiver."	^ start daysInYear! !!Month methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 12:09'!isLeapYear	^ start isLeapYear! !!Month methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 12:15'!monthAbbreviation	^ start monthAbbreviation! !!Month methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 12:17'!monthIndex	^ start monthIndex! !!Month methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 12:18'!monthName	^ start monthName! !!Month methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 12:32'!year	^start year! !!Month methodsFor: 'public protocol' stamp: 'jmv 4/9/2010 12:20'!yearNumber	^ start yearNumber! !!Schedule methodsFor: 'enumerating' stamp: 'jmv 4/9/2010 12:51'!between: aStart and: anEnd do: aBlock	| element end i requestedStartDate |	end _ self end min: anEnd.	element _ self start.		"Performance optimization. Avoid going through unnecesary days if easy."	requestedStartDate _ aStart date.	(requestedStartDate > element date and: [ self everyDayAtSameTimes ]) ifTrue: [		element _ DateAndTime date: requestedStartDate time: element time ].	i _ 1.	[ element < aStart ] whileTrue: [		element _ element + (schedule at: i).		i _ i + 1.		i > schedule size ifTrue: [i _ 1]].	i _ 1.	[ element <= end ] whileTrue: [		aBlock value: element.		element _ element + (schedule at: i).		i _ i + 1.		i > schedule size ifTrue: [i _ 1]]! !!Schedule methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 10:15'!includes: aDateAndTime	| dt |	self assert: aDateAndTime class = DateAndTime.	"Or else understand and fix"	dt _ aDateAndTime.	self scheduleDo: [ :e | e = dt ifTrue: [^true] ].	^ false.! !!Timespan class methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 12:34'!current	^ self including: DateAndTime now! !!Timespan class methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 12:34'!new	"Answer a Timespan starting on the Squeak epoch: 1 January 1901"	"Perhaps you need		Timespan current	"	^ self shouldNotImplement! !!Timespan class methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 11:12'!starting: aDateAndTime duration: aDuration	self classDefinesDuration ifTrue: [		self shouldNotImplement ].	self assert: aDateAndTime class == DateAndTime.	^ self basicNew 		start: aDateAndTime;		duration: aDuration;		yourself! !!Timespan class methodsFor: 'inquiries' stamp: 'jmv 4/9/2010 10:46'!classDefinesDuration	"Answer yes if we must have a fixed duration.	Examples of this are Date, Month, Week, Year"	^false! !!Timespan class methodsFor: 'inquiries' stamp: 'jmv 4/9/2010 10:43'!mustEncompassWholeDates	"Answer yes if we must start and end at midnight.	Examples of this are Date, Month, Week, Year"	^false! !!Timespan class methodsFor: 'instance creation' stamp: 'jmv 4/9/2010 11:24'!including: aDateAndTime	^ self starting: aDateAndTime duration: Duration zero! !!Timespan class methodsFor: 'instance creation' stamp: 'jmv 4/9/2010 12:54'!starting: startDateAndTime ending: endDateAndTime	self classDefinesDuration ifTrue: [		self shouldNotImplement ].	self assert: startDateAndTime class == DateAndTime.	self assert: endDateAndTime class == DateAndTime.	^ self 		starting: startDateAndTime 		duration: endDateAndTime - startDateAndTime! !!Date class methodsFor: 'smalltalk-80' stamp: 'jmv 4/9/2010 11:00'!today	^ DateAndTime now date! !!Date class methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 10:37'!julianDayNumber: aJulianDayNumber	^ (DateAndTime julianDayNumber: aJulianDayNumber) date! !!Date class methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 10:37'!year: year day: dayOfYear	^ (DateAndTime year: year day: dayOfYear) date! !!Date class methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 10:38'!year: year month: month day: day	^ (DateAndTime year: year month: month day: day) date! !!Date class methodsFor: 'general inquiries' stamp: 'jmv 4/9/2010 09:40'!orthodoxEasterDateFor: year "  compute the easter date according to the rules of the orthodox calendar.    source:     http://www.smart.net/~mmontes/ortheast.html   "     | r1 r2 r3 r4 ra rb r5 rc date |    r1 _ year \\ 19.    r2 _ year \\ 4.    r3 _ year \\ 7.    ra _ 19*r1 + 16.    r4 _ ra \\ 30.    rb _ r2 + r2 + (4*r3) + (6*r4).    r5 _ rb \\ 7.    rc _ r4 + r5.    date _ Date newDay: 3 month: 4 year: year.    ^date + rc days! !!Date class methodsFor: 'inquiries' stamp: 'jmv 4/9/2010 10:46'!classDefinesDuration	^true! !!Date class methodsFor: 'inquiries' stamp: 'jmv 4/9/2010 10:44'!mustEncompassWholeDates	^true! !!Date class methodsFor: 'instance creation' stamp: 'jmv 4/9/2010 11:34'!including: aDateAndTime	^self basicNew 		start: aDateAndTime midnight;		duration: (Duration days: 1);		yourself! !!Month class methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 11:51'!including: aDateAndTime	"Months start at day 1"	| adjusted days |	adjusted _ DateAndTime				year: aDateAndTime yearNumber				month: aDateAndTime monthIndex				day: 1.	days _ self daysInMonth: adjusted monthIndex forYear: adjusted yearNumber.	^ self basicNew 		start: adjusted;		duration: (Duration days: days);		yourself! !!Month class methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 12:33'!month: month year: year	"Create a Month for the given <year> and <month>.	<month> may be a number or a String with the	name of the month. <year> should be with 4 digits."	^ self including: (DateAndTime year: year month: month day: 1)! !!Month class methodsFor: 'inquiries' stamp: 'jmv 4/9/2010 10:46'!classDefinesDuration	^true! !!Month class methodsFor: 'inquiries' stamp: 'jmv 4/9/2010 10:44'!mustEncompassWholeDates	^true! !!Utilities class methodsFor: 'identification' stamp: 'jmv 4/9/2010 13:59'!monthDayTimeStringFrom: aSecondCount	| aDate aTime |	"From the date/time represented by aSecondCount, produce a string which indicates the date and time in the form:		ddMMMhhmmPP	  where:							dd is a two-digit day-of-month,							MMM is the alpha month abbreviation,							hhmm is the time,							PP is either am or pm          Utilities monthDayTimeStringFrom: Time primSecondsClock"	aDate _ Date fromSeconds: aSecondCount.	aTime _ Time fromSeconds: aSecondCount \\ 86400.	^ (aDate dayOfMonth asTwoCharacterString), 		(aDate monthName copyFrom: 1 to: 3), 		((aTime hour \\ 12) asTwoCharacterString), 		(aTime minute asTwoCharacterString),		(aTime hour > 12 ifTrue: ['pm'] ifFalse: ['am'])! !!Week class methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 11:28'!including: aDateAndTime	"Week will start from the 		Week startDay	"	| midnight delta adjusted |	midnight _ aDateAndTime midnight.	delta _ ((midnight dayOfWeek + 7 - (DayNames indexOf: self startDay)) rem: 7) abs.	adjusted _ midnight - delta days.	^ self basicNew 		start: adjusted;		duration: (Duration weeks: 1);		yourself! !!Week class methodsFor: 'inquiries' stamp: 'jmv 4/9/2010 10:46'!classDefinesDuration	^true! !!Week class methodsFor: 'inquiries' stamp: 'jmv 4/9/2010 10:44'!mustEncompassWholeDates	^true! !!Year methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 12:11'!daysInYear	^ self duration days! !!Year methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 11:59'!printOn: aStream	aStream nextPutAll: 'a Year ('.	self start yearNumber printOn: aStream.	aStream nextPutAll: ')'.! !!Year class methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 12:00'!current 	^ self yearNumber: (DateAndTime now yearNumber)! !!Year class methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 12:53'!including: aDateAndTime	"Warning: a Year can start at any date (different from Month and Week)"	| midnight |	midnight _ aDateAndTime midnight.	^ self basicNew 		start: midnight;		duration: (Duration days: (self daysInYear: midnight yearNumber));		yourself! !!Year class methodsFor: 'squeak protocol' stamp: 'jmv 4/9/2010 12:35'!yearNumber: aYear	^ self including: (DateAndTime year: aYear month: 1 day: 1).! !!Year class methodsFor: 'inquiries' stamp: 'jmv 4/9/2010 10:46'!classDefinesDuration	^true! !!Year class methodsFor: 'inquiries' stamp: 'jmv 4/9/2010 10:44'!mustEncompassWholeDates	^true! !!ZipArchiveMember methodsFor: 'private' stamp: 'jmv 4/9/2010 13:39'!dosToUnixTime: dt	"DOS years start at 1980, Unix at 1970, and Smalltalk at 1901.	So the Smalltalk seconds will be high by 69 years when used as Unix time_t values.	So shift 1980 back to 1911..."	| year mon mday hour min sec date time |	year _ (( dt bitShift: -25 ) bitAnd: 16r7F ) + 1911.	mon _ (( dt bitShift: -21 ) bitAnd: 16r0F ).	mday _ (( dt bitShift: -16 ) bitAnd: 16r1F ).	date _ Date newDay: mday month: mon year: year.	hour _ (( dt bitShift: -11 ) bitAnd: 16r1F ).	min _ (( dt bitShift: -5 ) bitAnd: 16r3F ).	sec _ (( dt bitShift: 1 ) bitAnd: 16r3E ).	time _ ((( hour * 60 ) + min ) * 60 ) + sec.	^date secondsSinceSqueakEpoch + time! !!ZipArchiveMember methodsFor: 'private' stamp: 'jmv 4/9/2010 14:00'!unixToDosTime: unixTime	| dosTime dateTime secs |	secs _ self unixToSqueakTime: unixTime.	"Squeak time (PST?)"	dateTime _ Time dateAndTimeFromSeconds: secs.	dosTime _ (dateTime second second) bitShift: -1.	dosTime _ dosTime + ((dateTime second minute) bitShift: 5).	dosTime _ dosTime + ((dateTime second hour) bitShift: 11).	dosTime _ dosTime + ((dateTime first dayOfMonth) bitShift: 16).	dosTime _ dosTime + ((dateTime first monthIndex) bitShift: 21).	dosTime _ dosTime + (((dateTime first yearNumber) - 1980) bitShift: 25).	^dosTime! !Year class removeSelector: #starting:!Year class removeSelector: #starting:duration:!Year class removeSelector: #year:!Year removeSelector: #asYear!Week class removeSelector: #starting:!Week class removeSelector: #starting:duration:!Week removeSelector: #asWeek!Week removeSelector: #index!Month class removeSelector: #starting:!Month class removeSelector: #starting:duration:!!Month class reorganize!('squeak protocol' including: month:year: readFrom:)('smalltalk-80' daysInMonth:forYear: indexOfMonth: nameOfMonth:)('inquiries' classDefinesDuration mustEncompassWholeDates)('instance creation')!Date class removeSelector: #starting:!Timespan class removeSelector: #starting:!Month removeSelector: #asMonth!Month removeSelector: #index!Month removeSelector: #previous!!Month reorganize!('squeak protocol' daysInMonth)('inquiries' name)('printing' printOn:)('public protocol' daysInYear isLeapYear monthAbbreviation monthIndex monthName year yearNumber)!Date removeSelector: #addDays:!Date removeSelector: #addMonths:!Date removeSelector: #asDate!Date removeSelector: #asSeconds!Date removeSelector: #leap!Date removeSelector: #onNextMonth!Date removeSelector: #onPreviousMonth!Date removeSelector: #subtractDate:!Date removeSelector: #subtractDays:!!Date reorganize!('printing' mmddyyyy printFormat: printOn: printOn:format: storeOn: yyyymmdd)('smalltalk-80' previous: weekday weekdayIndex)('squeak protocol' dayMonthYearDo:)('public protocol' dayOfMonth dayOfWeek dayOfWeekName dayOfYear daysInMonth daysInYear daysLeftInYear firstDayOfMonth isLeapYear julianDayNumber month monthAbbreviation monthIndex monthName secondsSinceSqueakEpoch week year yearNumber)!Timespan removeSelector: #<!Timespan removeSelector: #asDate!Timespan removeSelector: #asDateAndTime!Timespan removeSelector: #asDuration!Timespan removeSelector: #asMonth!Timespan removeSelector: #asTime!Timespan removeSelector: #asTimeStamp!Timespan removeSelector: #asWeek!Timespan removeSelector: #asYear!Timespan removeSelector: #dates!Timespan removeSelector: #datesDo:!Timespan removeSelector: #day!Timespan removeSelector: #dayOfMonth!Timespan removeSelector: #dayOfWeek!Timespan removeSelector: #dayOfWeekName!Timespan removeSelector: #dayOfYear!Timespan removeSelector: #daysInMonth!Timespan removeSelector: #daysInYear!Timespan removeSelector: #daysLeftInYear!Timespan removeSelector: #firstDayOfMonth!Timespan removeSelector: #isLeapYear!Timespan removeSelector: #julianDayNumber!Timespan removeSelector: #month!Timespan removeSelector: #monthAbbreviation!Timespan removeSelector: #monthIndex!Timespan removeSelector: #monthName!Timespan removeSelector: #months!Timespan removeSelector: #monthsDo:!Timespan removeSelector: #weeks!Timespan removeSelector: #weeksDo:!Timespan removeSelector: #year!Timespan removeSelector: #yearNumber!Timespan removeSelector: #years!Timespan removeSelector: #yearsDo:!!Timespan reorganize!('ansi protocol' + - = hash)('squeak protocol' duration end includes: includesAllOf: includesAnyOf: intersection: printOn: start start: to: union:)('enumerating' every:do: workDatesDo:)('private' do:with: do:with:when: duration:)('smalltalk-80' next previous)('public protocol')!Time removeSelector: #addSeconds:!Time removeSelector: #addTime:!Time removeSelector: #asDate!Time removeSelector: #asDateAndTime!Time removeSelector: #asDuration!Time removeSelector: #asMonth!Time removeSelector: #asNanoSeconds!Time removeSelector: #asSeconds!Time removeSelector: #asTime!Time removeSelector: #asTimeStamp!Time removeSelector: #asWeek!Time removeSelector: #asYear!Time removeSelector: #duration!Time removeSelector: #hours!Time removeSelector: #intervalString!Time removeSelector: #minutes!Time removeSelector: #seconds!Time removeSelector: #subtractTime:!Time removeSelector: #to:!TestRunner removeSelector: #formatTime:!String removeSelector: #asDateAndTime!String removeSelector: #asDuration!String removeSelector: #asTime!String removeSelector: #asTimeStamp!Integer removeSelector: #asYear!Number removeSelector: #asDuration!Number removeSelector: #day!Number removeSelector: #hour!Number removeSelector: #milliSecond!Number removeSelector: #minute!Number removeSelector: #nanoSecond!Number removeSelector: #second!Number removeSelector: #week!Duration class removeSelector: #month:!Duration removeSelector: #asDuration!Duration removeSelector: #asMilliSeconds!Duration removeSelector: #asNanoSeconds!Duration removeSelector: #asSeconds!!Duration reorganize!('public protocol' totalMilliSeconds totalNanoSeconds totalSeconds)('ansi protocol' * + - / < = abs days hash hours minutes negated negative positive seconds)('initialize-release' initialize)('squeak protocol' // \\ asDelay isZero nanoSeconds printOn: roundTo: truncateTo:)('private' seconds:nanoSeconds: storeOn: ticks)!DateAndTime removeSelector: #asDate!DateAndTime removeSelector: #asDateAndTime!DateAndTime removeSelector: #asDuration!DateAndTime removeSelector: #asMonth!DateAndTime removeSelector: #asNanoSeconds!DateAndTime removeSelector: #asSeconds!DateAndTime removeSelector: #asTime!DateAndTime removeSelector: #asTimeStamp!DateAndTime removeSelector: #asWeek!DateAndTime removeSelector: #asYear!DateAndTime removeSelector: #day!DateAndTime removeSelector: #duration!DateAndTime removeSelector: #hours!DateAndTime removeSelector: #middleOf:!DateAndTime removeSelector: #minutes!DateAndTime removeSelector: #offset:!DateAndTime removeSelector: #seconds!ChangeRecord removeSelector: #timeStamp!ArrayLiteralTest removeSelector: #array!Smalltalk removeClassNamed: #TimeStamp!