'From Cuis 1.0 of 26 March 2009 [latest update: #158] on 31 March 2009 at 1:39:59 pm'!!Object methodsFor: 'events-accessing' stamp: 'SqR 6/28/2001 13:19'!actionsDo: aBlock	self actionMap do: aBlock! !!Object methodsFor: 'macpal' stamp: 'sw 1/28/1999 17:31'!contentsChanged	self changed: #contents! !!Object methodsFor: 'Camp Smalltalk' stamp: 'SSS 7/3/2000 11:04'!sunitAddDependent: anObject        self addDependent: anObject! !!Object methodsFor: 'Camp Smalltalk' stamp: 'SSS 7/3/2000 11:04'!sunitChanged: anAspect        self changed: anAspect! !!Object methodsFor: 'Camp Smalltalk' stamp: 'SSS 7/3/2000 11:05'!sunitRemoveDependent: anObject        self removeDependent: anObject! !!ArrayedCollection methodsFor: 'adding'!add: newObject	self shouldNotImplement! !!Behavior methodsFor: 'testing' stamp: 'ar 7/11/1999 05:36'!shouldNotBeRedefined	"Return true if the receiver should not be redefined.	The assumption is that compact classes,	classes in Smalltalk specialObjects and 	Behaviors should not be redefined"	^(Smalltalk compactClassesArray includes: self)		or:[(Smalltalk specialObjectsArray includes: self)			or:[self isKindOf: self]]! !!Behavior methodsFor: 'Camp Smalltalk' stamp: 'SSS 7/3/2000 10:41'!sunitSelectors        ^self selectors asSortedCollection asOrderedCollection! !!BitBltSimulation methodsFor: 'primitives' stamp: 'ar 2/20/2001 21:12'!primitiveCopyBits	"Invoke the copyBits primitive. If the destination is the display, then copy it to the screen."	| rcvr |	self export: true.	rcvr _ interpreterProxy stackValue: interpreterProxy methodArgumentCount.	(self loadBitBltFrom: rcvr) 		ifFalse:[^interpreterProxy primitiveFail].	self copyBits.	interpreterProxy failed ifTrue:[^nil].	self showDisplayBits.	interpreterProxy failed ifTrue:[^nil].	interpreterProxy pop: interpreterProxy methodArgumentCount.	(combinationRule = 22) | (combinationRule = 32) ifTrue:[		interpreterProxy pop: 1.		^ interpreterProxy pushInteger: bitCount].! !!BitBltSimulation methodsFor: 'primitives' stamp: 'ar 2/19/2000 20:42'!primitiveDrawLoop	"Invoke the line drawing primitive."	| rcvr xDelta yDelta |	self export: true.	rcvr _ interpreterProxy stackValue: 2.	xDelta _ interpreterProxy stackIntegerValue: 1.	yDelta _ interpreterProxy stackIntegerValue: 0.	(self loadBitBltFrom: rcvr) 		ifFalse:[^interpreterProxy primitiveFail].	interpreterProxy failed ifFalse:[		self drawLoopX: xDelta Y: yDelta.		self showDisplayBits].	interpreterProxy failed ifFalse:[interpreterProxy pop: 2].! !!BlockContext methodsFor: 'Camp Smalltalk' stamp: 'SSS 7/3/2000 10:55'!sunitEnsure: aBlock        ^self ensure: aBlock! !!BlockContext methodsFor: 'Camp Smalltalk' stamp: 'SSS 7/3/2000 10:58'!sunitOn: anException do: aHandlerBlock        ^self on: anException do: aHandlerBlock! !!ChangeSet methodsFor: 'method changes' stamp: 'RAA 5/28/2001 13:43'!browseMessagesWithPriorVersions	"Open a message list browser on the new and changed methods in the receiver which have at least one prior version.  6/28/96 sw"	| aList |	aList _ self 		messageListForChangesWhich: [ :aClass :aSelector |			(VersionsBrowser versionCountForSelector: aSelector class: aClass) > 1		]		ifNone: [^self inform: 'None!!'].	Smalltalk 		browseMessageList: aList 		name: self name, ' methods that have prior versions'! !!ClassDescription methodsFor: 'instance variables' stamp: 'tk 12/12/2000 11:59'!renameSilentlyInstVar: old  to: new	| i oldName newName |	oldName _ old asString.	newName _ new asString.	(i _ instanceVariables indexOf: oldName) = 0 ifTrue:		[self error: oldName , ' is not defined in ', self name].	self allSuperclasses , self withAllSubclasses asOrderedCollection do:		[:cls | (cls instVarNames includes: newName) ifTrue:			[self error: newName , ' is already used in ', cls name]].	instanceVariables replaceFrom: i to: i with: (Array with: newName).	self replaceSilently: oldName to: newName.	"replace in text body of all methods"! !!ClassDescription methodsFor: 'compiling'!wantsChangeSetLogging	"Answer whether code submitted for the receiver should be remembered by the changeSet mechanism.  7/12/96 sw"	^ true! !!Class methodsFor: 'Camp Smalltalk' stamp: 'SSS 7/3/2000 10:59'!sunitName        ^self name! !!BitBltSimulation class methodsFor: 'system simulation' stamp: 'ar 4/24/2001 22:19'!copyBitsFrom: aBitBlt	"Simulate the copyBits primitive"	| proxy bb |	proxy _ InterpreterProxy new.	proxy loadStackFrom: thisContext sender home.	bb _ self simulatorClass new.	bb initialiseModule.	bb setInterpreter: proxy.	proxy success: (bb loadBitBltFrom: aBitBlt).	bb copyBits.	proxy failed ifFalse:[		proxy showDisplayBits: aBitBlt destForm 				Left: bb affectedLeft Top: bb affectedTop 				Right: bb affectedRight Bottom: bb affectedBottom].	^proxy stackValue: 0! !!BitBltSimulation class methodsFor: 'system simulation' stamp: 'ar 4/24/2001 22:20'!warpBitsFrom: aBitBlt	"Simulate the warpBits primitive"	| proxy bb |	proxy _ InterpreterProxy new.	proxy loadStackFrom: thisContext sender home.	bb _ self simulatorClass new.	bb initialiseModule.	bb setInterpreter: proxy.	proxy success: (bb loadWarpBltFrom: aBitBlt).	bb warpBits.	proxy failed ifFalse:[		proxy showDisplayBits: aBitBlt destForm 				Left: bb affectedLeft Top: bb affectedTop 				Right: bb affectedRight Bottom: bb affectedBottom].	^proxy stackValue: 0! !!BorderStyle class methodsFor: 'instance creation' stamp: 'ar 11/26/2001 14:59'!inset	^InsetBorder new! !!BorderStyle class methodsFor: 'instance creation' stamp: 'ar 11/26/2001 14:59'!raised	^RaisedBorder new! !!Class class methodsFor: 'instance creation' stamp: 'sw 4/27/2000 16:20'!templateForSubclassOf: priorClassName category: systemCategoryName 	"Answer an expression that can be edited and evaluated in order to define a new class, given that the class previously looked at was as given"	^ priorClassName asString, ' subclass: #NameOfSubclass	instanceVariableNames: ''''	classVariableNames: ''''	poolDictionaries: ''''	category: ''' , systemCategoryName asString , ''''! !!Color class methodsFor: 'other'!shutDown	"Color shutDown"	ColorChart _ nil.		"Palette of colors for the user to pick from"	CachedColormaps _ nil.	"Maps to translate between color depths"	MaskingMap _ nil.		"Maps all colors except transparent to black for creating a mask"! !!Date methodsFor: 'printing'!printOn: aStream	self printOn: aStream format: #(1 2 3 $  3 1 )! !!Date methodsFor: 'printing' stamp: 'di 9/22/2000 12:47'!storeOn: aStream	aStream print: self printString; nextPutAll: ' asDate'! !!Exception methodsFor: 'exceptionBuilder' stamp: 'tfei 6/4/1999 17:47'!messageText: signalerText	"Set an exception's message text."	messageText := signalerText! !!Exception methodsFor: 'exceptionDescription' stamp: 'tfei 6/6/1999 23:06'!defaultAction	"The default action taken if the exception is signaled."	self subclassResponsibility! !!Exception methodsFor: 'exceptionDescription' stamp: 'tfei 6/6/1999 23:09'!description	"Return a textual description of the exception."	| desc mt |	desc := self class name asString.	^(mt := self messageText) == nil		ifTrue: [desc]		ifFalse: [desc, ': ', mt]! !!Exception methodsFor: 'exceptionDescription' stamp: 'tfei 6/4/1999 17:40'!messageText	"Return an exception's message text."	^messageText! !!Exception methodsFor: 'exceptionDescription' stamp: 'tfei 11/19/1999 08:42'!tag       "Return an exception's tag value."       ^tag == nil               ifTrue: [self messageText]               ifFalse: [tag]! !!Exception methodsFor: 'Camp Smalltalk' stamp: 'SSS 7/3/2000 11:02'!sunitExitWith: aValue        self return: aValue! !!Exception class methodsFor: 'Camp Smalltalk' stamp: 'SSS 7/3/2000 11:02'!sunitSignalWith: aString        ^self signal: aString! !!ExceptionSet methodsFor: 'exceptionSelector' stamp: 'tfei 6/4/1999 18:37'!handles: anException	"Determine whether an exception handler will accept a signaled exception."	exceptions do:		[:ex |		(ex handles: anException)			ifTrue: [^true]].	^false! !!FontSet class methodsFor: 'filein/out' stamp: 'di 7/2/97 07:42'!fileOut	"FileOut and then change the properties of the file so that it won't be	treated as text by, eg, email attachment facilities"	super fileOut.	(FileStream oldFileNamed: self name , '.st') setFileTypeToObject; close! !!Form methodsFor: 'converting' stamp: 'ar 11/7/1999 20:29'!asMorph	^ImageMorph new image: self! !!LargeIntegersPlugin methodsFor: 'oop util' stamp: 'sr 3/11/2000 19:44'!bytes: aBytesObject growTo: newLen 	"Attention: this method invalidates all oop's!! Only newBytes is valid at    	     return."	"Does not normalize."	| newBytes oldLen copyLen |	self remapOop: aBytesObject in: [newBytes _ interpreterProxy instantiateClass: (interpreterProxy fetchClassOf: aBytesObject)					indexableSize: newLen].	oldLen _ self byteSizeOfBytes: aBytesObject.	oldLen < newLen		ifTrue: [copyLen _ oldLen]		ifFalse: [copyLen _ newLen].	self		cBytesCopyFrom: (interpreterProxy firstIndexableField: aBytesObject)		to: (interpreterProxy firstIndexableField: newBytes)		len: copyLen.	^ newBytes! !!LargeIntegersPlugin methodsFor: 'oop util' stamp: 'sr 1/23/2000 04:52'!bytesOrInt: oop growTo: len 	"Attention: this method invalidates all oop's!! Only newBytes is valid at    	          return."	| newBytes val class |	(interpreterProxy isIntegerObject: oop)		ifTrue: 			[val _ interpreterProxy integerValueOf: oop.			val < 0				ifTrue: [class _ interpreterProxy classLargeNegativeInteger]				ifFalse: [class _ interpreterProxy classLargePositiveInteger].			newBytes _ interpreterProxy instantiateClass: class indexableSize: len.			self cCopyIntVal: val toBytes: newBytes]		ifFalse: [newBytes _ self bytes: oop growTo: len].	^ newBytes! !!LargeIntegersPlugin methodsFor: 'oop functions' stamp: 'sr 3/11/2000 19:43'!bytes: aBytesOop Lshift: shiftCount 	"Attention: this method invalidates all oop's!! Only newBytes is valid at    	       return."	"Does not normalize."	| newBytes highBit newLen oldLen |	oldLen _ self byteSizeOfBytes: aBytesOop.	(highBit _ self cBytesHighBit: (interpreterProxy firstIndexableField: aBytesOop)				len: oldLen) = 0 ifTrue: [^ 0 asOop: SmallInteger].	newLen _ highBit + shiftCount + 7 // 8.	self remapOop: aBytesOop in: [newBytes _ interpreterProxy instantiateClass: (interpreterProxy fetchClassOf: aBytesOop)					indexableSize: newLen].	self		cBytesLshift: shiftCount		from: (interpreterProxy firstIndexableField: aBytesOop)		len: oldLen		to: (interpreterProxy firstIndexableField: newBytes)		len: newLen.	^ newBytes! !!LargeIntegersPlugin methodsFor: 'oop functions' stamp: 'sr 3/11/2000 19:44'!bytes: aBytesOop Rshift: anInteger bytes: b lookfirst: a 	"Attention: this method invalidates all oop's!! Only newBytes is valid at    	  return."	"Shift right 8*b+anInteger bits, 0<=n<8.         	Discard all digits beyond a, and all zeroes at or below a."	"Does not normalize."	| n x f m digit i oldLen newLen newBytes |	n _ 0 - anInteger.	x _ 0.	f _ n + 8.	i _ a.	m _ 255 bitShift: 0 - f.	digit _ self digitOfBytes: aBytesOop at: i.	[((digit bitShift: n)		bitOr: x)		= 0 and: [i ~= 1]]		whileTrue: 			[x _ digit bitShift: f.			"Can't exceed 8 bits"			i _ i - 1.			digit _ self digitOfBytes: aBytesOop at: i].	i <= b ifTrue: [^ interpreterProxy instantiateClass: (interpreterProxy fetchClassOf: aBytesOop)			indexableSize: 0"Integer new: 0 neg: self negative"].	"All bits lost"	oldLen _ self byteSizeOfBytes: aBytesOop.	newLen _ i - b.	self remapOop: aBytesOop in: [newBytes _ interpreterProxy instantiateClass: (interpreterProxy fetchClassOf: aBytesOop)					indexableSize: newLen].	"r _ Integer new: i - b neg: self negative."	"	count _ i.       	"	self		cCoreBytesRshiftCount: i		n: n		m: m		f: f		bytes: b		from: (interpreterProxy firstIndexableField: aBytesOop)		len: oldLen		to: (interpreterProxy firstIndexableField: newBytes)		len: newLen.	^ newBytes! !!LayoutProperties methodsFor: 'converting' stamp: 'ar 11/14/2000 17:52'!asTableLayoutProperties	^(TableLayoutProperties new)		hResizing: self hResizing;		vResizing: self vResizing;		disableTableLayout: self disableTableLayout;		yourself! !!MethodFinder methodsFor: 'arg maps' stamp: 'tk 4/24/1999 19:29'!data	^ data ! !!MethodNode methodsFor: 'converting'!decompileString 	"Answer a string description of the parse tree whose root is the receiver."	^ String streamContents: [:strm | self printOn: strm]! !!MethodNode methodsFor: 'converting'!decompileText 	"Answer a string description of the parse tree whose root is the receiver."	^ Text streamContents: [:strm | self printOn: strm]! !!Month methodsFor: 'inquiries' stamp: 'LC 7/26/1998 12:51'!index	^ self monthIndex! !!Morph methodsFor: 'accessing - extension' stamp: 'dgd 2/16/2003 19:57'!initializeExtension	"private - initializes the receiver's extension"	self privateExtension: MorphExtension new initialize! !!Morph methodsFor: 'debug and other' stamp: 'ar 4/2/1999 15:22'!resumeAfterStepError	"Resume stepping after an error has occured."	self startStepping. "Will #step"	self removeProperty:#errorOnStep. "Will remove prop only if #step was okay"! !!Morph methodsFor: 'event handling' stamp: 'tk 8/10/1998 16:04'!handlesMouseDown: evt	"Do I want to receive mouseDown events (mouseDown:, mouseMove:, mouseUp:)?"	"NOTE: The default response is false, except if you have added sensitivity to mouseDown events using the on:send:to: mechanism.  Subclasses that implement these messages directly should override this one to return true." 	self eventHandler ifNotNil: [^ self eventHandler handlesMouseDown: evt].	^ false! !!Morph methodsFor: 'halos and balloon help' stamp: 'di 9/18/97 14:01'!addOptionalHandlesTo: aHalo box: box	! !!Morph methodsFor: 'initialization' stamp: 'dgd 2/14/2003 17:30'!initialize	"initialize the state of the receiver"owner _ nil.	submorphs _ EmptyArray.	bounds _ self defaultBounds.		color _ self defaultColor! !!Morph methodsFor: 'meta-actions' stamp: 'ar 10/7/2000 18:44'!handlerForBlueButtonDown: anEvent	"Return the (prospective) handler for a mouse down event. The handler is temporarily installed and can be used for morphs further down the hierarchy to negotiate whether the inner or the outer morph should finally handle the event.	Note: Halos handle blue button events themselves so we will only be asked if there is currently no halo on top of us."	self wantsHaloFromClick ifFalse:[^nil].	anEvent handler ifNil:[^self].	anEvent handler isPlayfieldLike ifTrue:[^self]. "by default exclude playfields"	(anEvent shiftPressed)		ifFalse:[^nil] "let outer guy have it"		ifTrue:[^self] "let me have it"! !!Morph methodsFor: 'stepping and presenter' stamp: 'sw 10/20/1999 15:20'!stepAt: millisecondClockValue	"Do some periodic activity. Use startStepping/stopStepping to start and stop getting sent this message. The time between steps is specified by this morph's answer to the stepTime message.	The millisecondClockValue parameter gives the value of the millisecond clock at the moment of dispatch.	Default is to dispatch to the parameterless step method for the morph, but this protocol makes it possible for some morphs to do differing things depending on the clock value"		self step! !!BorderedMorph methodsFor: 'accessing' stamp: 'dgd 2/21/2003 22:42'!borderStyle: aBorderStyle 	"Work around the borderWidth/borderColor pair"	aBorderStyle = self borderStyle ifTrue: [^self].	"secure against invalid border styles"	(self canDrawBorder: aBorderStyle) 		ifFalse: 			["Replace the suggested border with a simple one"			^self borderStyle: (BorderStyle width: aBorderStyle width						color: (aBorderStyle trackColorFrom: self) color)].	aBorderStyle width = self borderStyle width ifFalse: [self changed].	(aBorderStyle isNil or: [aBorderStyle == BorderStyle default]) 		ifTrue: 			[self removeProperty: #borderStyle.			borderWidth := 0.			^self changed].	self setProperty: #borderStyle toValue: aBorderStyle.	borderWidth := aBorderStyle width.	borderColor := aBorderStyle style == #simple 				ifTrue: [aBorderStyle color]				ifFalse: [aBorderStyle style].	self changed! !!NewParagraph methodsFor: 'composition' stamp: 'jm 2/25/2003 16:20'!OLDcomposeLinesFrom: start to: stop delta: delta into: lineColl priorLines: priorLines atY: startingY 	"While the section from start to stop has changed, composition may ripple all the way to the end of the text.  However in a rectangular container, if we ever find a line beginning with the same character as before (ie corresponding to delta in the old lines), then we can just copy the old lines from there to the end of the container, with adjusted indices and y-values"	| charIndex lineY lineHeight scanner line row firstLine lineHeightGuess saveCharIndex hitCR maybeSlide sliding bottom priorIndex priorLine |	charIndex := start.	lines := lineColl.	lineY := startingY.	lineHeightGuess := textStyle lineGrid.	maxRightX := container left.	maybeSlide := stop < text size and: [container isMemberOf: Rectangle].	sliding := false.	priorIndex := 1.	bottom := container bottom.	scanner := CompositionScanner new text: text textStyle: textStyle.	firstLine := true.	[charIndex <= text size and: [lineY + lineHeightGuess <= bottom]] 		whileTrue: 			[sliding 				ifTrue: 					["Having detected the end of rippling recoposition, we are only sliding old lines"					priorIndex < priorLines size 						ifTrue: 							["Adjust and re-use previously composed line"							priorIndex := priorIndex + 1.							priorLine := (priorLines at: priorIndex) slideIndexBy: delta										andMoveTopTo: lineY.							lineColl addLast: priorLine.							lineY := priorLine bottom.							charIndex := priorLine last + 1]						ifFalse: 							["There are no more priorLines to slide."							sliding := maybeSlide := false]]				ifFalse: 					[lineHeight := lineHeightGuess.					saveCharIndex := charIndex.					hitCR := false.					row := container rectanglesAt: lineY height: lineHeight.					1 to: row size						do: 							[:i | 							(charIndex <= text size and: [hitCR not]) 								ifTrue: 									[line := scanner 												composeFrom: charIndex												inRectangle: (row at: i)												firstLine: firstLine												leftSide: i = 1												rightSide: i = row size.									lines addLast: line.									(text at: line last) = Character cr ifTrue: [hitCR := true].									lineHeight := lineHeight max: line lineHeight.	"includes font changes"									charIndex := line last + 1]].					lineY := lineY + lineHeight.					row notEmpty 						ifTrue: 							[lineY > bottom 								ifTrue: 									["Oops -- the line is really too high to fit -- back out"									charIndex := saveCharIndex.									row do: [:r | lines removeLast]]								ifFalse: 									["It's OK -- the line still fits."									maxRightX := maxRightX max: scanner rightX.									1 to: row size - 1										do: 											[:i | 											"Adjust heights across row if necess"											(lines at: lines size - row size + i) lineHeight: lines last lineHeight												baseline: lines last baseline].									charIndex > text size 										ifTrue: 											["end of text"											hitCR 												ifTrue: 													["If text ends with CR, add a null line at the end"													lineY + lineHeightGuess <= container bottom 														ifTrue: 															[row := container rectanglesAt: lineY height: lineHeightGuess.															row notEmpty 																ifTrue: 																	[line := (TextLine 																				start: charIndex																				stop: charIndex - 1																				internalSpaces: 0																				paddingWidth: 0)																				rectangle: row first;																				lineHeight: lineHeightGuess baseline: textStyle baseline.																	lines addLast: line]]].											lines := lines asArray.											^maxRightX].									firstLine := false]].											(maybeSlide and: [charIndex > stop]) 						ifTrue: 							["Check whether we are now in sync with previously composed lines"														[priorIndex < priorLines size 								and: [(priorLines at: priorIndex) first < (charIndex - delta)]] 									whileTrue: [priorIndex := priorIndex + 1].							(priorLines at: priorIndex) first = (charIndex - delta) 								ifTrue: 									["Yes -- next line will have same start as prior line."									priorIndex := priorIndex - 1.									maybeSlide := false.									sliding := true]								ifFalse: 									[priorIndex = priorLines size 										ifTrue: 											["Weve reached the end of priorLines,								so no use to keep looking for lines to slide."											maybeSlide := false]]]]].	firstLine 		ifTrue: 			["No space in container or empty text"			line := (TextLine 						start: start						stop: start - 1						internalSpaces: 0						paddingWidth: 0)						rectangle: (container topLeft extent: 0 @ lineHeightGuess);						lineHeight: lineHeightGuess baseline: textStyle baseline.			lines := Array with: line]		ifFalse: [self fixLastWithHeight: lineHeightGuess].	"end of container"	lines := lines asArray.	^maxRightX! !!Notification methodsFor: 'exceptionDescription' stamp: 'tfei 6/4/1999 18:18'!defaultAction	"No action is taken. The value nil is returned as the value of the message that signaled the exception."	^nil! !!PasteUpMorph methodsFor: 'user interface' stamp: 'dgd 2/22/2003 14:11'!modelWakeUp	"I am the model of a SystemWindow, that has just been activated"	| aWindow |	owner isNil ifTrue: [^self].	"Not in Morphic world"	(owner isKindOf: TransformMorph) ifTrue: [^self viewBox: self fullBounds].	(aWindow := self containingWindow) ifNotNil: 			[self viewBox = aWindow panelRect 				ifFalse: [self viewBox: aWindow panelRect]]! !!PasteUpMorph methodsFor: 'world menu' stamp: 'dgd 2/22/2003 14:10'!findDirtyBrowsers: evt 	"Present a menu of window titles for browsers with changes,	and activate the one that gets chosen."	| menu |	menu := MenuMorph new.	(SystemWindow windowsIn: self		satisfying: [:w | (w model isKindOf: Browser) and: [w model canDiscardEdits not]]) 			do: 				[:w | 				menu 					add: w label					target: w					action: #activate].	menu submorphs notEmpty ifTrue: [menu popUpEvent: evt in: self]! !!PasteUpMorph methodsFor: 'world menu' stamp: 'dgd 2/22/2003 14:10'!findDirtyWindows: evt 	"Present a menu of window titles for all windows with changes,	and activate the one that gets chosen."	| menu |	menu := MenuMorph new.	(SystemWindow windowsIn: self		satisfying: [:w | w model canDiscardEdits not]) do: 				[:w | 				menu 					add: w label					target: w					action: #activate].	menu submorphs notEmpty ifTrue: [menu popUpEvent: evt in: self]! !!PluggableButtonMorph methodsFor: 'private' stamp: 'dgd 2/21/2003 22:40'!getMenu: shiftPressed 	"Answer the menu for this button, supplying an empty menu to be filled in. If the menu selector takes an extra argument, pass in the current state of the shift key."	| menu |	getMenuSelector isNil ifTrue: [^nil].	menu := MenuMorph new defaultTarget: model.	getMenuSelector numArgs = 1 		ifTrue: [^model perform: getMenuSelector with: menu].	getMenuSelector numArgs = 2 		ifTrue: 			[^model 				perform: getMenuSelector				with: menu				with: shiftPressed].	^self error: 'The getMenuSelector must be a 1- or 2-keyword symbol'! !!PopUpMenu methodsFor: 'accessing'!labelString	^ labelString! !!PopUpMenu methodsFor: 'accessing'!lineArray	^ lineArray! !!PreDebugWindow methodsFor: 'initialization' stamp: 'aoy 2/15/2003 21:39'!initialize	| aFont proceedLabel debugLabel aWidth |	super initialize.	true 		ifFalse: 			["Preferences optionalMorphicButtons"			(aWidth := self widthOfFullLabelText) > 280 ifTrue: [^self].	"No proceed/debug buttons if title too long"			debugLabel := aWidth > 210 				ifTrue: 					["Abbreviated buttons if title pretty long"					proceedLabel := 'p'.					'd']				ifFalse: 					["Full buttons if title short enough"					proceedLabel := 'proceed'.					'debug'].			aFont := Preferences standardButtonFont.			self addMorph: (proceedButton := (SimpleButtonMorph new)								borderWidth: 0;								label: proceedLabel font: aFont;								color: Color transparent;								actionSelector: #proceed;								target: self).			proceedButton setBalloonText: 'continue execution'.			self addMorph: (debugButton := (SimpleButtonMorph new)								borderWidth: 0;								label: debugLabel font: aFont;								color: Color transparent;								actionSelector: #debug;								target: self).			debugButton setBalloonText: 'bring up a debugger'.			proceedButton submorphs first color: Color blue.			debugButton submorphs first color: Color red].	self adjustBookControls! !!Preferences class methodsFor: 'fonts' stamp: 'bp 6/13/2004 17:20'!chooseBalloonHelpFont	BalloonMorph chooseBalloonFont! !!Preferences class methodsFor: 'fonts' stamp: 'bp 6/13/2004 17:19'!standardBalloonHelpFont	^BalloonMorph balloonFont! !!ScrollPane methodsFor: 'pane events' stamp: 'di 5/7/1998 09:52'!handlesMouseDown: evt	^ true! !!PluggableListMorph methodsFor: 'as yet unclassified' stamp: 'ls 5/17/2001 09:04'!listMorphClass	^LazyListMorph! !!SelectionMenu methodsFor: 'access'!selections	^ selections! !!SimpleHierarchicalListMorph methodsFor: 'initialization' stamp: 'RAA 7/29/2000 22:15'!indentingItemClass		^IndentingListItemMorph! !!Slider methodsFor: 'geometry' stamp: 'jm 1/30/98 13:31'!sliderThickness	^ 7! !!SortedCollection methodsFor: 'private'!reSort	self sort: firstIndex to: lastIndex! !!String methodsFor: 'Camp Smalltalk' stamp: 'SSS 7/3/2000 11:09'!sunitAsSymbol        ^self asSymbol! !!String methodsFor: 'Camp Smalltalk' stamp: 'SSS 7/3/2000 11:10'!sunitMatch: aString        ^self match: aString! !!String methodsFor: 'Camp Smalltalk' stamp: 'SSS 7/3/2000 11:10'!sunitSubStrings        ^self substrings! !!StringHolder methodsFor: 'message list menu' stamp: 'tk 4/21/1998 07:57'!methodHierarchy	"Create and schedule a method browser on the hierarchy of implementors."	Utilities methodHierarchyBrowserForClass: self selectedClassOrMetaClass 			selector: self selectedMessageName! !!CodeHolder methodsFor: 'categories' stamp: 'sw 1/4/2001 12:04'!categoryOfCurrentMethod	"Answer the category that owns the current method.  If unable to determine a category, answer nil."	| aClass aSelector |	^ (aClass _ self selectedClassOrMetaClass) ifNotNil: [(aSelector _ self selectedMessageName) ifNotNil: [aClass whichCategoryIncludesSelector: aSelector]]! !!FileList class methodsFor: 'instance creation' stamp: 'sw 6/9/1999 12:29'!openFileDirectly	| aResult |	(aResult _ StandardFileMenu oldFile) ifNotNil:		[self openEditorOn: (aResult directory readOnlyFileNamed: aResult name) editString: nil]! !!Symbol methodsFor: 'testing' stamp: 'md 4/30/2003 15:31'!isSymbol	^ true ! !!Symbol methodsFor: 'Camp Smalltalk' stamp: 'SSS 7/3/2000 11:12'!sunitAsClass        ^SUnitNameResolver classNamed: self! !!SystemDictionary methodsFor: 'retrieving' stamp: 'sw 7/27/2001 18:39'!selectorsWithAnyImplementorsIn: selectorList  	"Answer the subset of the given list which represent method selectors which have at least one implementor in the system."	| good |	good _ OrderedCollection new.	self allBehaviorsDo:		[:class |			selectorList do:				[:aSelector |					(class includesSelector: aSelector) ifTrue:						[good add: aSelector]]].	^ good asSet asSortedArray"Smalltalk selectorsWithAnyImplementorsIn: #( contents contents: nuts)"! !!Text methodsFor: 'emphasis'!makeSelectorBoldIn: aClass	"For formatting Smalltalk source code, set the emphasis of that portion of 	the receiver's string that parses as a message selector to be bold."	| parser |	string size = 0 ifTrue: [^self].	(parser _ aClass parserClass new) parseSelector: string.	self makeBoldFrom: 1 to: (parser endOfLastToken min: string size)! !!TheWorldMenu methodsFor: 'construction' stamp: 'gm 2/28/2003 01:42'!alphabeticalMorphMenu	| list splitLists menu firstChar lastChar subMenu |	list := Morph withAllSubclasses select: [:m | m includeInNewMorphMenu].	list := list asArray sortBy: [:c1 :c2 | c1 name < c2 name].	splitLists := self splitNewMorphList: list depth: 3.	menu := MenuMorph new defaultTarget: self.	1 to: splitLists size		do: 			[:i | 			firstChar := i = 1 				ifTrue: [$A]				ifFalse: 					[((splitLists at: i - 1) last name first asInteger + 1) 								asCharacter].			lastChar := i = splitLists size 						ifTrue: [$Z]						ifFalse: [(splitLists at: i) last name first].			subMenu := MenuMorph new.			(splitLists at: i) do: 					[:cl | 					subMenu 						add: cl name						target: self						selector: #newMorphOfClass:event:						argument: cl].			menu add: firstChar asString , ' - ' , lastChar asString subMenu: subMenu].	^menu! !!Time methodsFor: 'printing' stamp: 'BP 5/17/2000 19:44'!hhmm24	"Return a string of the form 1123 (for 11:23 am), 2154 (for 9:54 pm), of exactly 4 digits"	^(String streamContents: 		[ :aStream | self print24: true showSeconds: false on: aStream ])			copyWithout: $:! !!Time methodsFor: 'printing' stamp: 'BP 5/17/2000 19:43'!print24	"Return as 8-digit string 'hh:mm:ss', with leading zeros if needed"	^String streamContents:		[ :aStream | self print24: true on: aStream ]! !!Time methodsFor: 'printing' stamp: 'BP 5/17/2000 19:41'!print24: hr24 on: aStream 	"Format is 'hh:mm:ss' or 'h:mm:ss am' "	self print24: hr24 showSeconds: true on: aStream ! !!Time methodsFor: 'printing' stamp: 'tk 9/7/2000 00:09'!printMinutes	"Return as string 'hh:mm pm'  "	^String streamContents:		[ :aStream | self print24: false showSeconds: false on: aStream ]! !!Time methodsFor: 'printing' stamp: 'di 9/22/2000 12:46'!storeOn: aStream	aStream print: self printString; nextPutAll: ' asTime'! !!Time class methodsFor: 'benchmarks' stamp: 'brp 8/24/2003 00:06'!benchmarkMillisecondClock		"Time benchmarkMillisecondClock"
	"Benchmark the time spent in a call to Time>>millisecondClockValue.
	On the VM level this tests the efficiency of calls to ioMSecs()."
	"PII/400 Windows 98: 0.725 microseconds per call"
	| temp1 temp2 temp3 delayTime nLoops time |
	delayTime _ 5000. "Time to run benchmark is approx. 2*delayTime"

	"Don't run the benchmark if we have an active delay since
	we will measure the additional penalty in the primitive dispatch
	mechanism (see #benchmarkPrimitiveResponseDelay)."
	Delay anyActive ifTrue:[
		^self notify:'Some delay is currently active.
Running this benchmark will not give any useful result.'].

	"Flush the cache for this benchmark so we will have
	a clear cache hit for each send to #millisecondClockValue below"
	Object flushCache.
	temp1 _ 0.
	temp2 _ self. "e.g., temp1 == Time"
	temp3 _ self millisecondClockValue + delayTime.

	"Now check how often we can run the following loop in the given time"
	[temp2 millisecondClockValue < temp3]
		whileTrue:[temp1 _ temp1 + 1].

	nLoops _ temp1. "Remember the loops we have run during delayTime"

	"Setup the second loop"
	temp1 _ 0.
	temp3 _ nLoops.

	"Now measure how much time we spend without sending #millisecondClockValue"
	time _ Time millisecondClockValue.
	[temp1 < temp3]
		whileTrue:[temp1 _ temp1 + 1].
	time _ Time millisecondClockValue - time.

	"And compute the number of microseconds spent per call to #millisecondClockValue"
	^((delayTime - time * 1000.0 / nLoops) truncateTo: 0.001) printString,
		' microseconds per call to Time>>millisecondClockValue'! !!Time class methodsFor: 'benchmarks' stamp: 'BP 3/30/2001 15:25'!benchmarkPrimitiveResponseDelay	"Time benchmarkPrimitiveResponseDelay"
	"Benchmark the overhead for primitive dispatches with an active Delay.
	On the VM level, this tests the efficiency of ioLowResMSecs."

	"PII/400 Windows98: 0.128 microseconds per prim"

	"ar 9/6/1999: This value is *extremely* important for stuff like sockets etc.
	I had a bad surprise when Michael pointed this particular problem out:
	Using the hardcoded clock() call for ioLowResMSecs on Win32 resulted in an overhead
	of 157.4 microseconds per primitive call - meaning you can't get no more than
	approx. 6000 primitives per second on my 400Mhz PII system with an active delay!!
	BTW, it finally explains why Squeak seemed soooo slow when running PWS or 
	other socket stuff. The new version (not using clock() but some Windows function) 
	looks a lot better (see above; approx. 8,000,000 prims per sec with an active delay)."

	| nLoops bb index baseTime actualTime delayTime |
	delayTime _ 5000. "Time to run this test is approx. 3*delayTime"

	Delay anyActive ifTrue:[
		^self notify:'Some delay is currently active.
Running this benchmark will not give any useful result.'].

	bb _ Array new: 1. "The object we send the prim message to"

	"Compute the # of loops we'll run in a decent amount of time"
	[(Delay forMilliseconds: delayTime) wait] 
		forkAt: Processor userInterruptPriority.

	nLoops _ 0.
	[Delay anyActive] whileTrue:[
		bb basicSize; basicSize; basicSize; basicSize; basicSize; 
			basicSize; basicSize; basicSize; basicSize; basicSize.
		nLoops _ nLoops + 1.
	].

	"Flush the cache and make sure #basicSize is in there"
	Object flushCache.
	bb basicSize.

	"Now run the loop without any active delay
	for getting an idea about its actual speed."
	baseTime _ self millisecondClockValue.
	index _ nLoops.
	[index > 0] whileTrue:[
		bb basicSize; basicSize; basicSize; basicSize; basicSize; 
			basicSize; basicSize; basicSize; basicSize; basicSize.
		index _ index - 1.
	].
	baseTime _ self millisecondClockValue - baseTime.

	"Setup the active delay but try to never make it active"
	[(Delay forMilliseconds: delayTime + delayTime) wait] 
		forkAt: Processor userInterruptPriority.

	"And run the loop"
	actualTime _ self millisecondClockValue.
	index _ nLoops.
	[index > 0] whileTrue:[
		bb basicSize; basicSize; basicSize; basicSize; basicSize; 
			basicSize; basicSize; basicSize; basicSize; basicSize.
		index _ index - 1.
	].
	actualTime _ self millisecondClockValue - actualTime.

	"And get us some result"
	^((actualTime - baseTime) * 1000 asFloat / (nLoops * 10) truncateTo: 0.001) printString,
		' microseconds overhead per primitive call'! !!Time class methodsFor: 'general inquiries' stamp: 'mir 10/29/1999 18:24'!millisecondsSince: lastTime	"Answer the elapsed time since last recorded in milliseconds.	Compensate for rollover."	^self milliseconds: self millisecondClockValue since: lastTime! !!Time class methodsFor: 'general inquiries' stamp: 'tk 10/3/2000 13:53'!namesForTimes: arrayOfSeconds	| simpleEnglish prev final prevPair myPair |	"Return English descriptions of the times in the array.  They are each seconds since 1901.  If two names are the same, append the date and time to distinguish them."	simpleEnglish _ arrayOfSeconds collect: [:secsAgo |		self humanWordsForSecondsAgo: self totalSeconds - secsAgo].	prev _ ''.	final _ simpleEnglish copy.	simpleEnglish withIndexDo: [:eng :ind | 		eng = prev ifFalse: [eng]			ifTrue: ["both say 'a month ago'"				prevPair _ self dateAndTimeFromSeconds: 						(arrayOfSeconds at: ind-1).				myPair _ self dateAndTimeFromSeconds: 						(arrayOfSeconds at: ind).				(final at: ind-1) = prev ifTrue: ["only has 'a month ago'"					final at: ind-1 put: 							(final at: ind-1), ', ', prevPair first mmddyyyy].				final at: ind put: 							(final at: ind), ', ', myPair first mmddyyyy.				prevPair first = myPair first 					ifTrue: [						(final at: ind-1) last == $m ifFalse: ["date but no time"							final at: ind-1 put: 								(final at: ind-1), ', ', prevPair second printMinutes].						final at: ind put: 							(final at: ind), ', ', myPair second printMinutes]].		prev _ eng].	^ final! !!UpdatingStringMorph methodsFor: 'accessing' stamp: 'sw 3/11/98 16:37'!growable	^ growable ~~ false! !!UpdatingStringMorph methodsFor: 'target access' stamp: 'tk 8/14/2000 23:11'!acceptValue: aValue	self updateContentsFrom: (self acceptValueFromTarget: aValue).! !!Utilities class methodsFor: 'common requests'!initialize	"Initialize the class variables.  5/16/96 sw"	self initializeCommonRequestStrings.	RecentSubmissions _ OrderedCollection new! !!WorldState methodsFor: 'update cycle' stamp: 'di 12/9/2000 10:58'!displayWorld: aWorld submorphs: submorphs	"Update this world's display."	| deferredUpdateMode worldDamageRects handsToDraw handDamageRects allDamage |	submorphs do: [:m | m fullBounds].  "force re-layout if needed"	self checkIfUpdateNeeded ifFalse: [^ self].  "display is already up-to-date"	deferredUpdateMode _ self doDeferredUpdatingFor: aWorld.	deferredUpdateMode ifFalse: [self assuredCanvas].	worldDamageRects _ self drawWorld: aWorld submorphs: submorphs invalidAreasOn: canvas.  "repair world's damage on canvas"	"self handsDo:[:h| h noticeDamageRects: worldDamageRects]."	handsToDraw _ self selectHandsToDrawForDamage: worldDamageRects.	handDamageRects _ handsToDraw collect: [:h | h savePatchFrom: canvas].	allDamage _ worldDamageRects, handDamageRects.	handsToDraw reverseDo: [:h | canvas fullDrawMorph: h].  "draw hands onto world canvas"	"*make this true to flash damaged areas for testing*"	Preferences debugShowDamage ifTrue: [aWorld flashRects: allDamage color: Color black].	canvas finish.	"quickly copy altered rects of canvas to Display:"	deferredUpdateMode		ifTrue: [self forceDamageToScreen: allDamage]		ifFalse: [canvas showAt: aWorld viewBox origin invalidRects: allDamage].	handsToDraw do: [:h | h restoreSavedPatchOn: canvas].  "restore world canvas under hands"	Display deferUpdates: false; forceDisplayUpdate.! !!ZeroDivide methodsFor: 'exceptionDescription' stamp: 'tfei 6/5/1999 17:26'!isResumable	"Determine whether an exception is resumable."	^true! !Utilities initialize!