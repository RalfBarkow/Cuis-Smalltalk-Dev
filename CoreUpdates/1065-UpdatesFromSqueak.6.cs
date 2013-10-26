'From Cuis 3.3 of 2 June 2011 [latest update: #1024] on 28 August 2011 at 10:30:32 pm'!!Object methodsFor: 'error handling' stamp: 'jmv 8/28/2011 21:45'!doesNotUnderstand: aMessage 	 "Handle the fact that there was an attempt to send the given	  message to the receiver but the receiver does not understand	  this message (typically sent from the machine when a message	 is sent to the receiver and no method is defined for that selector).		Question: Why is this method different from the one inherited from ProtoObject?	Answer (eem):		This is intentional.  Martin's reply is one half of the issue, that you want		to be able to proceed after defining a method in  the debugger.  The other		half is that you want to be able to catch doesNotUnderstand: in an exception		handler and proceed with a result, e.g.	[nil zork]		on: MessageNotUnderstood		do: [:ex|			ex message selector == #zork ifTrue:				[ex resume: #ok].			ex pass]		evaluates to #ok.			jmv adds:		The real difference is what happens if the exception is eventually handled by the default handler		(i.e. the debugger is opened). In that case, don't allow the user to proceed.	"	"Testing: 		(3 activeProcess)	"	| exception resumeValue |	(exception _ MessageNotUnderstood new)		message: aMessage;		receiver: self.	resumeValue _ exception signal.	^exception reachedDefaultHandler		ifTrue: [ aMessage sentTo: self ]		ifFalse: [ resumeValue ]! !!Behavior methodsFor: 'accessing class hierarchy' stamp: 'jmv 8/28/2011 16:20'!allSubclassesWithLevelDo: classAndLevelBlock startingLevel: level 	"Walk the tree of subclasses, giving the class and its level"	classAndLevelBlock value: self value: level.	self == Class ifTrue:  [^ self].  "Don't visit all the metaclasses"	"Visit subclasses in alphabetical order"	self subclasses		sort: [ :a :b | a name <= b name ];		do: [ :subclass | 			subclass				allSubclassesWithLevelDo: classAndLevelBlock				startingLevel: level + 1 ]! !!Behavior methodsFor: 'testing method dictionary' stamp: 'jmv 8/28/2011 22:12'!thoroughWhichSelectorsReferTo: literal special: specialFlag byte: specialByte	"Answer a set of selectors whose methods access the argument as a 	literal. Dives into the compact literal notation, making it slow but 	thorough "	| who |	who _ Set new.	self selectorsAndMethodsDo:		[:sel :method |		((method hasLiteralThorough: literal) or: [specialFlag and: [method scanFor: specialByte]])			ifTrue:				[((literal isVariableBinding) not					or: [method sendsToSuper not					"N.B. (method indexOfLiteral: literal) < method numLiterals copes with looking for					Float bindingOf: #NaN, since (Float bindingOf: #NaN) ~= (Float bindingOf: #NaN)."					or: [(method indexOfLiteral: literal) ~= 0]])						ifTrue: [who add: sel]]].	^ who! !!Behavior methodsFor: 'testing method dictionary' stamp: 'jmv 8/28/2011 22:12'!whichSelectorsReferTo: literal special: specialFlag byte: specialByte	"Answer a set of selectors whose methods access the argument as a literal."	| who |	Preferences thoroughSenders ifTrue: [		^self thoroughWhichSelectorsReferTo: literal special: specialFlag byte: specialByte ].	who _ Set new.	self selectorsAndMethodsDo: 		[:sel :method |		((method hasLiteral: literal) or: [specialFlag and: [method scanFor: specialByte]])			ifTrue:				[((literal isVariableBinding) not					or: [method sendsToSuper not					"N.B. (method indexOfLiteral: literal) < method numLiterals copes with looking for					Float bindingOf: #NaN, since (Float bindingOf: #NaN) ~= (Float bindingOf: #NaN)."					or: [(method indexOfLiteral: literal) ~= 0]])						ifTrue: [who add: sel]]].	^ who! !!Behavior methodsFor: 'private' stamp: 'jmv 8/28/2011 22:07'!spaceUsed	"Answer a rough estimate of number of bytes used by this class and its metaclass. Does not include space used by class variables."	| space |	space _ 0.	self selectorsDo: [ :sel | | method |		space _ space + 16.  "dict and org'n space"		method _ self compiledMethodAt: sel.		space _ space + (method size + 6 "hdr + avg pad").		method literalsDo: [ :lit |			(lit isMemberOf: Array) ifTrue: [ space _ space + ((lit size + 1) * 4)].			(lit isMemberOf: Float) ifTrue: [ space _ space + 12].			(lit isMemberOf: String) ifTrue: [ space _ space + (lit size + 6)].			(lit isMemberOf: LargeNegativeInteger) ifTrue: [ space _ space + ((lit size + 1) * 4)].			(lit isMemberOf: LargePositiveInteger) ifTrue: [ space _ space + ((lit size + 1) * 4)]]].	^ space! !!Browser methodsFor: 'class list' stamp: 'jmv 8/28/2011 16:11'!selectedClass	"Answer the class that is currently selected. Answer nil if no selection 	exists."	| name envt |	(name _ self selectedClassName) ifNil: [^ nil].	(envt _ self selectedEnvironment) ifNil: [^ nil].	^ envt at: name ifAbsent: nil! !!FileDirectory methodsFor: 'testing' stamp: 'jmv 8/28/2011 16:13'!exists"Answer whether the directory exists"	| result |	result _ self primLookupEntryIn: pathName index: 1.	^ result ~~ #badDirectoryPath! !!FileDirectory methodsFor: 'private' stamp: 'jmv 8/28/2011 16:12'!directoryContentsFor: fullPath	"Return a collection of directory entries for the files and directories in the directory with the given path. See primLookupEntryIn:index: for further details."	"FileDirectory default directoryContentsFor: ''"	| entries index done entryArray |	entries _ OrderedCollection new: 200.	index _ 1.	done _ false.	[done] whileFalse: [		entryArray _ self primLookupEntryIn: fullPath index: index.		#badDirectoryPath == entryArray ifTrue: [			^(InvalidDirectoryError pathName: pathName) signal].		entryArray			ifNil: [done _ true]			ifNotNil: [entries addLast: (DirectoryEntry fromArray: entryArray)].		index _ index + 1].	^ entries asArray! !!FileStream class methodsFor: 'instance creation' stamp: 'jmv 8/28/2011 21:59'!new	^ self basicNew initialize! !!Integer methodsFor: 'mathematical functions' stamp: 'jmv 8/28/2011 21:55'!raisedTo: n modulo: m	"Answer the modular exponential.	Note: this implementation is optimized for case of large integers raised to large powers.		Implementation notes:	#raisedTo:modulo: now uses the new montgomeryTimes primitive if present.	Otherwise, it fallbacks to classical multiplication, remainder sequences.	The Montgomery algorithm avoids division implied by the modulo operation and thus save a few CPU cycles (see 	http://en.wikipedia.org/wiki/Montgomery_reduction for an introduction).	Both Montgomery and fallback version use a sliding window algorithm.	It consists in exponentiating by bit packets rather than bit by bit and then save some multiplications.	It is a classical CPU vs memory tradeoff. Hope the comments help if you're interested.	Note that raisedTo: could also use a slidingWindow if we want to. Do we?	In such case, we would have 3 most identical methods and should think of a better refactoring."	| a s mInv |	n = 0 ifTrue: [^1].	(self >= m or: [self < 0]) ifTrue: [^self \\ m raisedTo: n modulo: m].	n < 0 ifTrue: [^(self reciprocalModulo: m) raisedTo: n negated modulo: m].	(n < 4096 or: [m even])		ifTrue: [			"Overhead of Montgomery method might cost more than naive divisions, use naive"			^self slidingLeftRightRaisedTo: n modulo: m].		mInv _ 256 - ((m bitAnd: 255) reciprocalModulo: 256). 	"Initialize the result to R=256 raisedTo: m digitLength"	a _ (1 bitShift: m digitLength*8) \\ m.		"Montgomerize self (multiply by R)"	(s _ self montgomeryTimes: (a*a \\ m) modulo: m mInvModB: mInv)		ifNil: [			"No Montgomery primitive available ? fallback to naive divisions"			^self slidingLeftRightRaisedTo: n modulo: m].	"Exponentiate self*R"	a _ s montgomeryRaisedTo: n times: a modulo: m mInvModB: mInv.	"Demontgomerize the result (divide by R)"	^a montgomeryTimes: 1 modulo: m mInvModB: mInv! !!MessageSet methodsFor: 'message functions' stamp: 'jmv 8/28/2011 22:25'!reformulateList	"The receiver's messageList has been changed; rebuild it"	super reformulateList.	self initializeMessageList: messageList.	self changed: #messageList.	self changed: #messageListIndex.	self acceptedContentsChanged.	autoSelectString ifNotNil: [		self changed: #autoSelect]! !!Month class methodsFor: 'squeak protocol' stamp: 'jmv 8/28/2011 16:34'!readFrom: aStream	| m y c |	m _ (ReadWriteStream with: '') reset.	[(c _ aStream next) isSeparator] whileFalse: [m nextPut: c].	[(c _ aStream next) isSeparator] whileTrue.	y _ (ReadWriteStream with: '') reset.	y nextPut: c.	[aStream atEnd] whileFalse: [y nextPut: aStream next].	^ self 		month: m contents		year: y contents asInteger"Month readFrom: 'July 1998' readStream"! !!Paragraph methodsFor: 'private' stamp: 'jmv 8/28/2011 21:34'!fastFindFirstLineSuchThat: lineBlock	"Perform a binary search of the lines array and return the index	of the first element for which lineBlock evaluates as true.	This assumes the condition is one that goes from false to true for	increasing line numbers (as, eg, yval > somey or start char > somex).	If lineBlock is not true for any element, return size+1."	^lines		findBinaryIndex: [ :each | 			(lineBlock value: each)				ifTrue: [ -1 ]				ifFalse: [ 1 ] ]		do: [ :found | found ]		ifNone: [ :lower :upper | upper ]! !!SystemDictionary methodsFor: 'retrieving' stamp: 'jmv 8/28/2011 22:18'!allMethodsWithString: aString	"Answer a sorted Collection of all the methods that contain, in a string literal, aString as a substring.  2/1/96 sw.  The search is case-sensitive, and does not dive into complex literals, confining itself to string constants.	5/2/96 sw: fixed so that duplicate occurrences of aString in the same method don't result in duplicated entries in the browser"	| aStringSize list |	aStringSize _ aString size.	list _ Set new.	Cursor wait showWhile: [		self allBehaviorsDo: [ :class |			class selectorsDo: [ :sel |				sel ~~ #DoIt ifTrue: [					(class compiledMethodAt: sel) literalsDo: [ :aLiteral |						((aLiteral isMemberOf: String) and: [ aLiteral size >= aStringSize ]) ifTrue: [							(aLiteral								findString: aString								startingAt: 1) > 0 ifTrue: [ list add: class name , ' ' , sel ]]]]]]].	^ list asArray sort.! !!UpdatingMenuItemMorph methodsFor: 'world' stamp: 'jmv 8/28/2011 21:51'!updateContents	"Update the receiver's contents"	| newString enablement nArgs |	((wordingProvider isNil) or: [wordingSelector isNil]) ifFalse: [		nArgs _ wordingSelector numArgs.		newString _ nArgs = 0			ifTrue: [				wordingProvider perform: wordingSelector]			ifFalse: [				(nArgs = 1 and: [wordingArgument notNil])					ifTrue: [						wordingProvider perform: wordingSelector with: wordingArgument]					ifFalse: [						nArgs = arguments size ifTrue: [							wordingProvider perform: wordingSelector withArguments: arguments]]].		newString = (self contentString ifNil: [ contents ])			ifFalse: [				self contents: newString.				Theme current decorateMenu: owner ]].	enablementSelector ifNotNil: [		(enablement _ self enablement) == isEnabled 			ifFalse:	[self isEnabled: enablement]]! !Pen removeSelector: #print:withFont:!