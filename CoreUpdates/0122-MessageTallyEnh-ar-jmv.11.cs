'From Squeak3.7 of ''4 September 2004'' [latest update: #5989] on 4 March 2009 at 3:14:29 pm'!!classDefinition: #MessageTally category: #'Tools-Debugger'!Magnitude subclass: #MessageTally	instanceVariableNames: 'class method tally receivers senders time gcStats maxClassNameSize maxClassPlusSelectorSize maxTabs process '	classVariableNames: 'DefaultPollPeriod ObservedProcess Timer ShowProcesses '	poolDictionaries: ''	category: 'Tools-Debugger'!!MessageTally methodsFor: 'collecting leaves' stamp: 'jmv 3/4/2009 13:48'!into: leafDict fromSender: senderTally	| leafNode |	leafNode _ leafDict at: method		ifAbsent: [leafDict at: method			put: ((MessageTally new class: class method: method)				process: process)].	leafNode bump: tally fromSender: senderTally! !!MessageTally methodsFor: 'collecting leaves' stamp: 'jmv 3/4/2009 14:33'!leavesInto: leafDict fromSender: senderTally	| rcvrs |	rcvrs _ self sonsOver: 0.	rcvrs size = 0		ifTrue: [self into: leafDict fromSender: senderTally]		ifFalse: [						"Do not show 'other processes' "			"Please keep consistency with #rootPrintOn:total:totalTime:tallyExact:orThreshold: 			on showing them or not!!"			rcvrs anyOne process ifNil: [^self].						rcvrs do:				[:node |				node isPrimitives					ifTrue: [node leavesInto: leafDict fromSender: senderTally]					ifFalse: [node leavesInto: leafDict fromSender: self]]]! !!MessageTally methodsFor: 'comparing' stamp: 'ar 3/3/2009 19:36'!= aMessageTally	self species == aMessageTally species ifFalse: [^ false].	^ aMessageTally method == method and:[aMessageTally process == process]! !!MessageTally methodsFor: 'comparing' stamp: 'jmv 3/4/2009 13:48'!sonsOver: threshold	| hereTally last sons |	(receivers == nil or: [receivers size = 0]) ifTrue: [^#()].	hereTally _ tally.	sons _ receivers select:  "subtract subNode tallies for primitive hits here"		[:son |		hereTally _ hereTally - son tally.		son tally > threshold].	hereTally > threshold		ifTrue: 			[last _ MessageTally new class: class method: method.			last process: process.			^sons copyWith: (last primitives: hereTally)].	^sons! !!MessageTally methodsFor: 'initialize-release' stamp: 'jmv 3/4/2009 10:07'!close	(Timer isMemberOf: Process) ifTrue: [Timer terminate].	Timer _ nil.	class _ method _ tally _ receivers _ nil! !!MessageTally methodsFor: 'initialize-release' stamp: 'jmv 3/4/2009 10:06'!spyAllEvery: millisecs on: aBlock	"Create a spy and spy on the given block at the specified rate."	"Spy all the system processes"	| myDelay startTime time0 observedProcess |	(aBlock isMemberOf: BlockContext)		ifFalse: [self error: 'spy needs a block here'].	self class: aBlock receiver class method: aBlock method.		"set up the probe"	myDelay := Delay forMilliseconds: millisecs.	time0 := Time millisecondClockValue.	gcStats _ Smalltalk getVMParameters.	Timer _ [		[true] whileTrue: [			startTime := Time millisecondClockValue.			myDelay wait.			observedProcess := Processor preemptedProcess.			self tally: observedProcess suspendedContext				in: (ShowProcesses ifTrue: [observedProcess])				"tally can be > 1 if ran a long primitive"				by: (Time millisecondClockValue - startTime) // millisecs].		nil] newProcess.	Timer priority: Processor timingPriority-1.		"activate the probe and evaluate the block"	Timer resume.	^ aBlock ensure: [		"Collect gc statistics"		Smalltalk getVMParameters keysAndValuesDo: [ :idx :gcVal | 			gcStats at: idx put: (gcVal - (gcStats at: idx))].		"cancel the probe and return the value"		Timer terminate.		time := Time millisecondClockValue - time0]! !!MessageTally methodsFor: 'initialize-release' stamp: 'jmv 3/4/2009 15:14'!spyEvery: millisecs on: aBlock	"Create a spy and spy on the given block at the specified rate."	"Spy only on the active process (in which aBlock is run)"	| myDelay startTime time0 observedProcess |	(aBlock isMemberOf: BlockContext)		ifFalse: [self error: 'spy needs a block here'].	self class: aBlock receiver class method: aBlock method.		"set up the probe"	observedProcess _ Processor activeProcess.	myDelay := Delay forMilliseconds: millisecs.	time0 := Time millisecondClockValue.	gcStats _ Smalltalk getVMParameters.	Timer _ [		[true] whileTrue: [			startTime := Time millisecondClockValue.			myDelay wait.			self tally: Processor preemptedProcess suspendedContext				in: (ShowProcesses ifTrue: [					observedProcess == Processor preemptedProcess ifTrue: [observedProcess] ifFalse: [nil]])				"tally can be > 1 if ran a long primitive"				by: (Time millisecondClockValue - startTime) // millisecs].		nil] newProcess.	Timer priority: Processor timingPriority-1.		"activate the probe and evaluate the block"	Timer resume.	^ aBlock ensure: [		"Collect gc statistics"		Smalltalk getVMParameters keysAndValuesDo: [ :idx :gcVal | 			gcStats at: idx put: (gcVal - (gcStats at: idx))].		"cancel the probe and return the value"		Timer terminate.		time := Time millisecondClockValue - time0]! !!MessageTally methodsFor: 'initialize-release' stamp: 'jmv 3/4/2009 14:24'!spyEvery: millisecs onProcess: aProcess forMilliseconds: msecDuration 	"Create a spy and spy on the given process at the specified rate."	| myDelay startTime time0 endTime observedProcess sem |	(aProcess isKindOf: Process)		ifFalse: [self error: 'spy needs a Process here'].	self class: aProcess suspendedContext receiver class method: aProcess suspendedContext method.	"set up the probe"	observedProcess _ aProcess.	myDelay _ Delay forMilliseconds: millisecs.	time0 _ Time millisecondClockValue.	endTime _ time0 + msecDuration.	sem _ Semaphore new.	gcStats _ Smalltalk getVMParameters.	Timer _ [			[				startTime _ Time millisecondClockValue.				myDelay wait.				self tally: Processor preemptedProcess suspendedContext					in: (ShowProcesses ifTrue: [						observedProcess == Processor preemptedProcess ifTrue: [observedProcess] ifFalse: [nil]])					"tally can be > 1 if ran a long primitive"					by: (Time millisecondClockValue - startTime) // millisecs.				startTime < endTime			] whileTrue.			sem signal.		] newProcess.	Timer priority: Processor timingPriority-1.		"activate the probe and evaluate the block"	Timer resume.	"activate the probe and wait for it to finish"	sem wait.	"Collect gc statistics"	Smalltalk getVMParameters keysAndValuesDo: [ :idx :gcVal | 		gcStats at: idx put: (gcVal - gcStats at: idx)].	time _ Time millisecondClockValue - time0! !!MessageTally methodsFor: 'printing' stamp: 'ar 3/3/2009 19:43'!fullPrintOn: aStream tallyExact: isExact orThreshold: perCent	| threshold |  	isExact ifFalse: [threshold _ (perCent asFloat / 100 * tally) rounded].	aStream nextPutAll: '**Tree**'; cr.	self rootPrintOn: aStream		total: tally		totalTime: time		tallyExact: isExact		orThreshold: threshold.	aStream nextPut: Character newPage; cr.	aStream nextPutAll: '**Leaves**'; cr.	self leavesPrintOn: aStream		tallyExact: isExact		orThreshold: threshold! !!MessageTally methodsFor: 'printing' stamp: 'jmv 3/4/2009 14:34'!rootPrintOn: aStream total: total totalTime: totalTime tallyExact: isExact orThreshold: threshold 	| sons groups p |	ShowProcesses ifFalse:[		^self treePrintOn: aStream			tabs: OrderedCollection new			thisTab: ''			total: total			totalTime: totalTime			tallyExact: isExact			orThreshold: threshold.	].	sons := isExact ifTrue: [receivers] ifFalse: [self sonsOver: threshold].	groups := sons groupBy:[:aTally| aTally process] having:[:g| true].	groups do:[:g|		sons := g asSortedCollection.		p _ g anyOne process.		"Do not show 'other processes' "		"Please keep consistency with #leavesInto:fromSender: 		on showing them or not!!"		p ifNotNil: [			aStream nextPutAll: '--------------------------------'; cr.			aStream nextPutAll: 'Process: ',  (p ifNil: [ 'other processes'] ifNotNil: [ p browserPrintString]); cr.			aStream nextPutAll: '--------------------------------'; cr.			(1 to: sons size) do:[:i | 				(sons at: i) 					treePrintOn: aStream					tabs: OrderedCollection new					thisTab: ''					total: total					totalTime: totalTime					tallyExact: isExact					orThreshold: threshold]].	].! !!MessageTally methodsFor: 'reporting' stamp: 'jmv 3/4/2009 09:27'!report: strm 	"Print a report, with cutoff percentage of each element of the tree 	(leaves, roots, tree), on the stream, strm."	self report: strm cutoff: 1! !!MessageTally methodsFor: 'tallying' stamp: 'jmv 3/4/2009 09:42'!tally: context by: count	"Explicitly tally the specified context and its stack."	| sender |		"Add to this node if appropriate"	context method == method ifTrue: [^self bumpBy: count].		"No sender? Add new branch to the tree."	(sender _ context home sender)ifNil: [		^ (self bumpBy: count) tallyPath: context by: count].		"Find the node for the sending context (or add it if necessary)"	^ (self tally: sender by: count) tallyPath: context by: count! !!MessageTally methodsFor: 'tallying' stamp: 'jmv 3/4/2009 10:37'!tally: context in: aProcess by: count	"Explicitly tally the specified context and its stack."	| sender |	"Add to this node if appropriate"	context method == method ifTrue: [^self bumpBy: count].		"No sender? Add new branch to the tree."	(sender _ context home sender) ifNil: [		^ (self bumpBy: count) tallyPath: context in: aProcess by: count].		"Find the node for the sending context (or add it if necessary)"	^ (self tally: sender in: aProcess by: count) tallyPath: context in: aProcess by: count! !!MessageTally methodsFor: 'tallying' stamp: 'jmv 3/4/2009 09:45'!tallyPath: context by: count	| aMethod path |	aMethod _ context method.		"Find the correct child (if there)"	receivers do: [ :oldTally | 		oldTally method == aMethod ifTrue: [path _ oldTally]].		"Add new child if needed"	path ifNil: [		path _ MessageTally new class: context receiver class method: aMethod.		receivers _ receivers copyWith: path].		^ path bumpBy: count! !!MessageTally methodsFor: 'tallying' stamp: 'jmv 3/4/2009 09:46'!tallyPath: context in: aProcess by: count	| aMethod path |	aMethod _ context method.		"Find the correct child (if there)"	receivers do: [ :oldTally | 		(oldTally method == aMethod and: [oldTally process == aProcess])			ifTrue: [path _ oldTally]].			"Add new child if needed"	path ifNil:[		path _ MessageTally new class: context receiver class method: aMethod;			process: aProcess;			maxClassNameSize: maxClassNameSize;			maxClassPlusSelectorSize: maxClassPlusSelectorSize;			maxTabs: maxTabs.		receivers _ receivers copyWith: path].	^ path bumpBy: count! !!MessageTally methodsFor: 'private' stamp: 'jmv 3/4/2009 13:47'!copyWithTally: hitCount	^ (MessageTally new class: class method: method) 		process: process;		bump: hitCount! !!MessageTally methodsFor: 'private' stamp: 'ar 3/3/2009 19:29'!process	^process! !!MessageTally methodsFor: 'private' stamp: 'ar 3/3/2009 19:29'!process: aProcess	process := aProcess! !!MessageTally class methodsFor: 'spying' stamp: 'jmv 3/4/2009 09:59'!spyAllOn: aBlock	"Spy on all the processes in the system		[1000 timesRepeat: [3.14159 printString. Processor yield]] fork.	[1000 timesRepeat: [20 factorial. Processor yield]] fork.	[1000 timesRepeat: [20 factorial. Processor yield]] fork.	MessageTally spyAllOn: [ (Delay forMilliseconds: 100) wait]		"	| node result |	node _ self new.	result _ node spyAllEvery: self defaultPollPeriod on: aBlock.	(StringHolder new contents: (String streamContents: [:s | node report: s; close]))		openLabel: 'Spy Results'.	^ result! !!MessageTally class methodsFor: 'spying' stamp: 'jmv 3/4/2009 15:01'!spyOn: aBlock	"	[1000 timesRepeat: [		100 timesRepeat: [120 factorial].		(Delay forMilliseconds: 10) wait		]] forkAt: 45 named: '45'.	MessageTally spyOn: [10000 timesRepeat: [1.23 printString]]	"	| node result |	node _ self new.	result _ node spyEvery: self defaultPollPeriod on: aBlock.	(StringHolder new contents: (String streamContents: [:s | node report: s; close]))		openLabel: 'Spy Results'.	^ result! !!MessageTally class methodsFor: 'spying' stamp: 'jmv 3/4/2009 15:03'!spyOnProcess: aProcess forMilliseconds: msecDuration 	"	| p1 p2 |  	p1 _ [100000 timesRepeat: [3.14159 printString. Processor yield]] fork.  	p2 _ [100000 timesRepeat: [3.14159 printString. Processor yield]] fork.  	(Delay forMilliseconds: 100) wait.  	MessageTally spyOnProcess: p1 forMilliseconds: 1000	"	| node |	node _ self new.	node		spyEvery: self defaultPollPeriod		onProcess: aProcess		forMilliseconds: msecDuration.	(StringHolder new		contents: (String				streamContents: [:s | node report: s;						 close]))		openLabel: 'Spy Results'! !!MessageTally class methodsFor: 'defaults' stamp: 'jmv 3/2/2009 12:32'!defaultMaxTabs	"Return the default number of tabs after which leading white space is compressed"	^120! !!MessageTally class methodsFor: 'defaults' stamp: 'jmv 3/4/2009 10:29'!showProcesses	"Indicates whether to show each process separately or cumulatively.	For example, compare the spy results of the following with both values:			[1000 timesRepeat: [3.14159 printString. Processor yield]] fork.		[1000 timesRepeat: [30 factorial. Processor yield]] fork.		[1000 timesRepeat: [30 factorial. Processor yield]] fork.		MessageTally spyAllOn: [ (Delay forMilliseconds: 100) wait] 	"	^ShowProcesses! !!MessageTally class methodsFor: 'defaults' stamp: 'jmv 3/4/2009 10:29'!showProcesses: aBool	"Indicates whether to show each process separately or cumulatively.	For example, compare the spy results of the following with both values:			[1000 timesRepeat: [3.14159 printString. Processor yield]] fork.		[1000 timesRepeat: [30 factorial. Processor yield]] fork.		[1000 timesRepeat: [30 factorial. Processor yield]] fork.		MessageTally spyAllOn: [ (Delay forMilliseconds: 100) wait]	"	ShowProcesses := aBool.! !!MessageTally class methodsFor: 'class initialization' stamp: 'jmv 3/4/2009 09:24'!initialize	"MessageTally initialize"	"By default, show each process separately"	ShowProcesses := true! !!OldTheWorldMenu methodsFor: 'commands' stamp: 'jmv 3/2/2009 11:34'!startMessageTally	"Tally on all the processes in the system, and not only the UI"		(self confirm: 'MessageTally all the processes inthe system, until the mouse pointergoes to the top of the screen') ifTrue: [		MessageTally spyAllOn: [			[Sensor peekMousePt y > 0] whileTrue: [World doOneCycle]]]! !!OldTheWorldMenu methodsFor: 'commands' stamp: 'jmv 3/2/2009 11:39'!startThenBrowseMessageTally	"Tally only the UI process"		(self confirm: 'MessageTally the UI process until themouse pointer goes to the top of the screen')		ifTrue: [TimeProfileBrowser				onBlock: [[Sensor peekMousePt y > 10]						whileTrue: [World doOneCycle]]]! !!OldTheWorldMenu methodsFor: 'construction' stamp: 'jmv 3/2/2009 12:26'!debugMenu        | menu |        menu _ self menu: 'debug...'.        ^self fillIn: menu from: {                 { 'inspect world' . { #myWorld . #inspect } }.                { 'explore world' . { #myWorld . #explore } }.                { 'MessageTally all Processes' . { self . #startMessageTally } }.                { 'MessageTally UI and browse' . { self . #startThenBrowseMessageTally } }.                { 'open process browser' . { ProcessBrowser . #open } }.                nil.                        "(self hasProperty: #errorOnDraw) ifTrue:  Later make this come up only when needed."                { 'start drawing again' . { #myWorld . #resumeAfterDrawError } }.                { 'start stepping again' . { #myWorld . #resumeAfterStepError } }.        }! !MessageTally initialize!!classDefinition: #MessageTally category: #'Tools-Debugger'!Magnitude subclass: #MessageTally	instanceVariableNames: 'class method process tally receivers senders time gcStats maxClassNameSize maxClassPlusSelectorSize maxTabs'	classVariableNames: 'DefaultPollPeriod ShowProcesses Timer'	poolDictionaries: ''	category: 'Tools-Debugger'!