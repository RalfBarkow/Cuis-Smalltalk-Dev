'From Cuis 3.0 of 14 January 2011 [latest update: #763] on 17 January 2011 at 9:23:52 am'!!Bag methodsFor: 'enumerating' stamp: 'jmv 1/16/2011 23:58'!asSet	"Answer a set with the elements of the receiver"	 ^contents keys asSet! !!Behavior methodsFor: 'accessing method dictionary' stamp: 'dvf 9/27/2005 17:08'!methodDict: aDictionary	methodDict := aDictionary! !!Behavior methodsFor: 'accessing method dictionary' stamp: 'jmv 1/17/2011 00:07'!selectors	"Answer a Set of all the message selectors specified in the receiver's 	method dictionary."	^ self methodDict keys asSet	"Point selectors."! !!Behavior methodsFor: 'testing method dictionary' stamp: 'jmv 1/17/2011 00:06'!whichSelectorsAccess: instVarName 	"Answer a collection (an Array) of selectors whose methods access the argument, 	instVarName, as a named instance variable."	| instVarIndex |	instVarIndex _ self allInstVarNames indexOf: instVarName ifAbsent: [^Set new].	^ (self methodDict keys select: [ :sel | 		((self methodDict at: sel)			readsField: instVarIndex)			or: [(self methodDict at: sel) writesField: instVarIndex]]) asSet	"Point whichSelectorsAccess: 'x'."! !!Behavior methodsFor: 'testing method dictionary' stamp: 'jmv 1/17/2011 00:06'!whichSelectorsStoreInto: instVarName 	"Answer a Set of selectors whose methods access the argument, 	instVarName, as a named instance variable."	| instVarIndex |	instVarIndex _ self allInstVarNames indexOf: instVarName ifAbsent: [^Set new].	^ (self methodDict keys select: 		[:sel | (self methodDict at: sel) writesField: instVarIndex]) asSet	"Point whichSelectorsStoreInto: 'x'."! !!BytecodeEncoder methodsFor: 'results' stamp: 'jmv 1/16/2011 23:53'!schematicTempNamesString	"Answer the temp names for the current method node in a form that captures	 temp structure.  The temps at each method and block scope level occurr	 space-separated, with any indirect temps enclosed in parentheses.  Each block	 level is enclosed in square brackets.  e.g.		'method level temps (indirect temp)[block args and temps (indirect)]'	 This representation can be reconstituted into a blockExtentsToTempsMap	 by a CompiledMethod that has been copied with teh schematicTempNamesString."	blockExtentsToLocals ifNil:		[self error: 'blockExtentsToLocals uninitialized.  method not yet generated?'].	^String streamContents: [ :aStream |		self printSchematicTempNamesOn: aStream			blockExtents: (blockExtentsToLocals keys sort: [ :range1 :range2|							range1 first <= range2 first])			fromIndex: 1]! !!Categorizer methodsFor: 'private' stamp: 'jmv 1/17/2011 00:10'!setDefaultList: anArray	categoryArray _ Array with: Default.	categoryStops _ Array with: anArray size.	elementArray _ anArray! !!ChangeSet methodsFor: 'accessing' stamp: 'jmv 1/17/2011 00:48'!classRemoves	"Unlike some related methods, answer an Array (not a Set)"	^ changeRecords keys select: [ :className |		(changeRecords at: className) isClassRemoval]! !!ChangeSet methodsFor: 'fileIn/Out' stamp: 'jmv 1/17/2011 00:02'!fileOutOn: stream 	"Write out all the changes the receiver knows about"	| classList |	(self isEmpty and: [stream isKindOf: FileStream])		ifTrue: [self inform: 'Warning: no changes to file out'].	classList _ ChangeSet superclassOrder: self changedClasses asOrderedCollection.	"First put out rename, max classDef and comment changes."	classList do: [:aClass | self fileOutClassDefinition: aClass on: stream].	"Then put out all the method changes"	classList do: [:aClass | self fileOutChangesFor: aClass on: stream].	"Finally put out removals, final class defs and reorganization if any"	classList reverseDo: [:aClass | self fileOutPSFor: aClass on: stream].	self classRemoves sort do: [ :aClassName |		stream nextChunkPut: 'Smalltalk removeClassNamed: #', aClassName; cr]! !!ClassChangeRecord methodsFor: 'method changes' stamp: 'jmv 1/17/2011 00:05'!changedSelectors	"Return a set of the changed or removed selectors."	^ methodChanges keys asSet! !!ClassDescription methodsFor: 'printing' stamp: 'jmv 1/16/2011 23:54'!classVariablesString	"Answer a string of my class variable names separated by spaces."	^String streamContents: [ :stream | 		self classPool keys sort 			do: [ :each | stream nextPutAll: each ]			separatedBy: [ stream space ] ]! !!ClassDescription methodsFor: 'organization' stamp: 'jmv 1/16/2011 23:54'!organization	"Answer the instance of ClassOrganizer that represents the organization 	of the messages of the receiver."	organization ifNil:		[self organization: (ClassOrganizer defaultList: self methodDict keys sort)].	(organization isMemberOf: Array) ifTrue:		[self recoverFromMDFaultWithTrace].		"Making sure that subject is set correctly. It should not be necessary."	organization ifNotNil: [organization setSubject: self].	^ organization! !!ClassDescription methodsFor: 'fileIn/Out' stamp: 'jmv 1/17/2011 00:08'!moveChangesTo: newFile 	"Used in the process of condensing changes, this message requests that 	the source code of all methods of the receiver that have been changed 	should be moved to newFile."	| changes |	changes _ self methodDict keys select: [:sel | (self methodDict at: sel) fileIndex > 1].	self fileOutChangedMessages: changes asSet		on: newFile		moveSource: true		toFile: 2! !!Class methodsFor: 'class variables' stamp: 'jmv 1/17/2011 00:05'!classVarNames	"Answer a Set of the names of the class variables defined in the receiver."	^self classPool keys asSet! !!Class methodsFor: 'fileIn/Out' stamp: 'jmv 1/16/2011 23:54'!fileOutPool: aPool onFileStream: aFileStream 	| aPoolName |	(aPool  isKindOf: SharedPool class) ifTrue:[^self notify: 'we do not fileout SharedPool type shared pools for now'].	aPoolName _ Smalltalk keyAtIdentityValue: aPool.	Transcript cr; show: aPoolName.	aFileStream nextPutAll: 'Transcript show: ''' , aPoolName , '''; cr!!'; cr.	aFileStream nextPutAll: 'Smalltalk at: #' , aPoolName , ' put: Dictionary new!!'; cr.	aPool keys sort do: [ :aKey | | aValue |		aValue _ aPool at: aKey.		aFileStream nextPutAll: aPoolName , ' at: #''' , aKey asString , '''', ' put:  '.		(aValue isKindOf: Number)			ifTrue: [aValue printOn: aFileStream]			ifFalse: [aFileStream nextPutAll: '('.					aValue printOn: aFileStream.					aFileStream nextPutAll: ')'].		aFileStream nextPutAll: '!!'; cr].	aFileStream cr! !!AbstractFont class methodsFor: 'instance accessing' stamp: 'jmv 1/16/2011 23:53'!familyNames	"	AbstractFont familyNames	"	^AvailableFonts keys sort! !!AbstractFont class methodsFor: 'instance accessing' stamp: 'jmv 1/16/2011 23:53'!pointSizesFor: aString	"	AbstractFont pointSizesFor: 'DejaVu'	"	| familyDictionary |	familyDictionary _ AvailableFonts at: aString ifAbsent: [^#()].	^familyDictionary keys sort! !!AbstractSound class methodsFor: 'sound library' stamp: 'jmv 1/16/2011 23:53'!soundNames	^ Sounds keys sort! !!AbstractSound class methodsFor: 'sound library-file in/out' stamp: 'jmv 1/16/2011 23:57'!unloadSampledTimbres	"This can be done to unload those bulky sampled timbres to shrink the image. The unloaded sounds are replaced by a well-known 'unloaded sound' object to enable the unloaded sounds to be detected when the process is reversed."	"AbstractSound unloadSampledTimbres"	Sounds keys do: [:soundName |		(((Sounds at: soundName) isKindOf: SampledInstrument) or:		 [(Sounds at: soundName) isKindOf: LoopedSampledSound]) ifTrue: [			Sounds at: soundName put: self unloadedSound]].	self updateScorePlayers.	Smalltalk garbageCollect! !!Categorizer class methodsFor: 'instance creation' stamp: 'jmv 1/17/2011 00:11'!defaultList: anArray 	"Answer an instance of me with initial elements from the argument, 	aSortedCollection."	^self new setDefaultList: anArray! !!BasicClassOrganizer class methodsFor: 'instance creation' stamp: 'jmv 1/17/2011 00:11'!class: aClassDescription defaultList: anArray	| inst |	inst _ self defaultList: anArray.	inst setSubject: aClassDescription.	^ inst! !!ClassBuilder class methodsFor: 'cleanup obsolete classes' stamp: 'jmv 1/16/2011 23:54'!cleanupClassHierarchyFor: aClassDescription		| myName mySuperclass |	mySuperclass _ aClassDescription superclass.	(self isReallyObsolete: aClassDescription) ifTrue: [				"Remove class >>>from SystemDictionary if it is obsolete"		myName _ aClassDescription name asString.		Smalltalk keys do: [:each | 			(each asString = myName and: [(Smalltalk at: each) == aClassDescription])				ifTrue: [Smalltalk removeKey: each]].		"Make class officially obsolete if it is not"		(aClassDescription name asString beginsWith: 'AnObsolete')			ifFalse: [aClassDescription obsolete].		aClassDescription isObsolete 			ifFalse: [self error: 'Something wrong!!'].		"Add class to obsoleteSubclasses of its superclass"		mySuperclass			ifNil: [self error: 'Obsolete subclasses of nil cannot be stored'].		(mySuperclass obsoleteSubclasses includes: aClassDescription)			ifFalse: [mySuperclass addObsoleteSubclass: aClassDescription].	] ifFalse:[		"check if superclass has aClassDescription in its obsolete subclasses"		mySuperclass ifNil:[mySuperclass _ Class]. "nil subclasses"		mySuperclass removeObsoleteSubclass: aClassDescription.	].	"And remove its obsolete subclasses if not actual superclass"	aClassDescription obsoleteSubclasses do:[:obs|		obs superclass == aClassDescription ifFalse:[			aClassDescription removeObsoleteSubclass: obs]].! !!ClassOrganizer methodsFor: 'accessing' stamp: 'jmv 1/17/2011 00:10'!setDefaultList: anArray	| oldDict oldCategories |	oldDict _ self elementCategoryDict.	oldCategories _ self categories copy.	SystemChangeNotifier uniqueInstance doSilently: [		super setDefaultList: anArray].	self notifyOfChangedSelectorsOldDict: oldDict newDict: self elementCategoryDict.	self notifyOfChangedCategoriesFrom: oldCategories to: self categories.! !!ClosureCompilerTest methodsFor: 'tests' stamp: 'jmv 1/17/2011 00:13'!testBlockNumbering	"Test that the compiler and CompiledMethod agree on the block numbering of a substantial doit."	"self new testBlockNumbering"	| methodNode method tempRefs |	methodNode _		Parser new			encoderClass: EncoderForV3PlusClosures;			parse: 'foo					| numCopiedValuesCounts |					numCopiedValuesCounts := Dictionary new.					0 to: 32 do: [:i| numCopiedValuesCounts at: i put: 0].					Transcript clear.					Smalltalk allClassesDo:						[:c|						{c. c class} do:							[:b|							Transcript nextPut: b name first; endEntry.							b selectorsAndMethodsDo:								[:s :m| | pn |								m isQuick not ifTrue:									[pn := b parserClass new												encoderClass: EncoderForV3PlusClosures;												parse: (b sourceCodeAt: s)												class: b.									 pn generate: #(0 0 0 0).									 [pn accept: nil]										on: MessageNotUnderstood										do: [:ex| | msg numCopied |											msg := ex message.											(msg selector == #visitBlockNode:											 and: [(msg argument instVarNamed: ''optimized'') not]) ifTrue:												[numCopied := (msg argument computeCopiedValues: pn) size.												 numCopiedValuesCounts													at: numCopied													put: (numCopiedValuesCounts at: numCopied) + 1].											msg setSelector: #==.											ex resume: nil]]]]].					numCopiedValuesCounts'			class: Object.	method _ methodNode generate: #(0 0 0 0).	tempRefs _ methodNode encoder blockExtentsToTempsMap.	self assert: tempRefs keys asSet = method startpcsToBlockExtents values asSet! !!ClosureCompilerTest methodsFor: 'tests' stamp: 'jmv 1/17/2011 00:13'!testBlockNumberingForInjectInto	"Test that the compiler and CompiledMethod agree on the block numbering of Collection>>inject:into:	 and that temp names for inject:into: are recorded."	"self new testBlockNumberingForInjectInto"	| methodNode method tempRefs |	methodNode := Parser new						encoderClass: EncoderForV3PlusClosures;						parse: (Collection sourceCodeAt: #inject:into:)						class: Collection.	method := methodNode generate: #(0 0 0 0).	tempRefs := methodNode encoder blockExtentsToTempsMap.	self assert: tempRefs keys asSet = method startpcsToBlockExtents values asSet.	self assert: ((tempRefs includesKey: (0 to: 6))				and: [(tempRefs at: (0 to: 6)) hasEqualElements: #(('thisValue' 1) ('binaryBlock' 2) ('nextValue' (3 1)))]).	self assert: ((tempRefs includesKey: (2 to: 4))				and: [(tempRefs at: (2 to: 4)) hasEqualElements: #(('each' 1) ('binaryBlock' 2) ('nextValue' (3 1)))])! !!ClosureCompilerTest methodsFor: 'tests' stamp: 'jmv 1/17/2011 00:13'!testMethodAndNodeTempNames	"self new testMethodAndNodeTempNames"	"Test that BytecodeAgnosticMethodNode>>blockExtentsToTempRefs answers the same	 structure as CompiledMethod>>blockExtentsToTempRefs when the method has been	 copied with the appropriate temps.  This tests whether doit methods are debuggable	 since they carry their own temps."	self closureCases do:		[:source| | mn om m mbe obe |		mn := source first isLetter					ifTrue:						[self class compilerClass new							compile: source							in: self class							notifying: nil							ifFail: [self error: 'compilation error']]					ifFalse:						[self class compilerClass new							compileNoPattern: source							in: self class							context: nil							notifying: nil							ifFail: [self error: 'compilation error']].		m := (om := mn generate: #(0 0 0 0)) copyWithTempsFromMethodNode: mn.		self assert: m holdsTempNames.		self assert: m endPC = om endPC.		mbe := m blockExtentsToTempsMap.		obe := mn blockExtentsToTempsMap.		self assert: mbe keys asSet = obe keys asSet.		(mbe keys intersection: obe keys) do:			[:interval|			self assert: (mbe at: interval) = (obe at: interval)]]! !!Decompiler methodsFor: 'initialize-release' stamp: 'jmv 1/16/2011 23:55'!mapFromBlockStartsIn: aMethod toTempVarsFrom: schematicTempNamesString constructor: aDecompilerConstructor	| map |	map := aMethod				mapFromBlockKeys: aMethod startpcsToBlockExtents keys sort				toSchematicTemps: schematicTempNamesString.	map keysAndValuesDo:		[:startpc :tempNameTupleVector|		tempNameTupleVector isEmpty ifFalse:			[| subMap numTemps tempVector |			subMap := Dictionary new.			"Find how many temp slots there are (direct & indirect temp vectors)			 and for each indirect temp vector find how big it is."			tempNameTupleVector do:				[:tuple|				tuple last isArray					ifTrue:						[subMap at: tuple last first put: tuple last last.						 numTemps := tuple last first]					ifFalse:						[numTemps := tuple last]].			"create the temp vector for this scope level."			tempVector := Array new: numTemps.			"fill it in with any indirect temp vectors"			subMap keysAndValuesDo:				[:index :size|				tempVector at: index put: (Array new: size)].			"fill it in with temp nodes."			tempNameTupleVector do:				[:tuple| | itv |				tuple last isArray					ifTrue:						[itv := tempVector at: tuple last first.						 itv at: tuple last last							put: (aDecompilerConstructor									codeTemp: tuple last last - 1									named: tuple first)]					ifFalse:						[tempVector							at: tuple last							put: (aDecompilerConstructor									codeTemp: tuple last - 1									named: tuple first)]].			"replace any indirect temp vectors with proper RemoteTempVectorNodes"			subMap keysAndValuesDo:				[:index :size|				tempVector					at: index					put: (aDecompilerConstructor							codeRemoteTemp: index							remoteTemps: (tempVector at: index))].			"and update the entry in the map"			map at: startpc put: tempVector]].	^map! !!Dictionary methodsFor: 'user interface' stamp: 'jmv 1/16/2011 23:55'!explorerContentsWithIndexCollect: twoArgBlock	| sortedKeys |	sortedKeys _ self keys sort: [:x :y |		((x isString and: [y isString])			or: [x isNumber and: [y isNumber]])			ifTrue: [x < y]			ifFalse: [x class == y class				ifTrue: [x printString < y printString]				ifFalse: [x class name < y class name]]].	^ sortedKeys collect: [:k | twoArgBlock value: (self at: k) value: k].! !!FileContentsBrowser methodsFor: 'class list' stamp: 'jmv 1/16/2011 23:55'!classList	"Answer an array of the class names of the selected category. Answer an 	empty array if no selection exists."	^(systemCategoryListIndex = 0 or: [ self selectedPackage isNil ])		ifTrue: [ #() ]		ifFalse: [ self selectedPackage classes keys sort ]! !!Parser methodsFor: 'error correction' stamp: 'jmv 1/17/2011 00:19'!declareUndeclaredTemps: methodNode	"Declare any undeclared temps, declaring them at the smallest enclosing scope."	| undeclared userSelection blocksToVars |	(undeclared _ encoder undeclaredTemps) isEmpty ifTrue: [ ^ self ].	userSelection _ requestor selectionInterval.	blocksToVars _ IdentityDictionary new.	undeclared do: [ :var |		(blocksToVars			at: (var tag == #method				ifTrue: [ methodNode block ]				ifFalse: [ methodNode accept: (VariableScopeFinder new ofVariable: var) ])			ifAbsentPut: [ SortedCollection new ]) add: var name ].	(blocksToVars removeKey: methodNode block ifAbsent: nil) ifNotNil: [ :rootVars |		rootVars do: [ :varName |			self pasteTempAtMethodLevel: varName ]].	(blocksToVars keys sort: [ :a :b |		a tempsMark < b tempsMark ]) do: [ :block | | decl |		decl _ String streamContents: [ :strm |			(blocksToVars at: block) do: [ :v |				strm nextPutAll: v; nextPut: $  ]].		block temporaries isEmpty			ifTrue: [				self					substituteWord: ' | ' , decl , '|'					wordInterval: (block tempsMark + 1 to: block tempsMark)					offset: requestorOffset ]			ifFalse: [				self					substituteWord: decl					wordInterval: (block tempsMark to: block tempsMark - 1)					offset: requestorOffset ]].	requestor		selectInvisiblyFrom: userSelection first		to: userSelection last + requestorOffset.	ReparseAfterSourceEditing signal! !!PseudoClass methodsFor: 'accessing' stamp: 'jmv 1/17/2011 00:12'!organization	organization ifNil: [organization _ PseudoClassOrganizer defaultList: #()].	"Making sure that subject is set correctly. It should not be necessary."	organization setSubject: self.	^ organization! !!PseudoClass methodsFor: 'methods' stamp: 'jmv 1/17/2011 00:07'!selectors	^self sourceCode keys asSet! !!PseudoClassOrganizer methodsFor: 'accessing' stamp: 'jmv 1/17/2011 00:11'!setDefaultList: anArray	super setDefaultList: anArray.	self classComment: nil! !!SampledSound class methodsFor: 'sound library' stamp: 'jmv 1/16/2011 23:55'!soundNames	"Answer a list of sound names for the sounds stored in the sound library."	"| s |	 SampledSound soundNames asSortedCollection do: [:n |		n asParagraph display.		s _ SampledSound soundNamed: n.		s ifNotNil: [s playAndWaitUntilDone]]"	^ SoundLibrary keys! !!StrikeFont class methodsFor: 'removing' stamp: 'jmv 1/17/2011 00:22'!removeForPDA"StrikeFont removeForPDA"	| familyDict |	familyDict _ AvailableFonts at: 'DejaVu'.	familyDict keys do: [ :k |		(#(5 6 7 8 9) includes: k) 			ifTrue: [				(familyDict at: k) derivativeFont: nil at: 0 ]			ifFalse: [				familyDict removeKey: k ]].		Preferences setDefaultFonts: #(		(setSystemFontTo: 'DejaVu' 8)		(setListFontTo: 'DejaVu' 6)		(setMenuFontTo: 'DejaVu' 7)		(setWindowTitleFontTo: 'DejaVu' 9)		(setBalloonHelpFontTo: 'DejaVu' 7)		(setCodeFontTo: 'DejaVu' 7)		(setButtonFontTo: 'DejaVu' 7))! !!StrikeFont class methodsFor: 'removing' stamp: 'jmv 1/17/2011 00:22'!removeMostFonts"StrikeFont removeMostFonts"	| familyDict |	familyDict _ AvailableFonts at: 'DejaVu'.	familyDict keys do: [ :k |		(#(8 10 12 14 16 18 20) includes: k) 			ifTrue: [				(familyDict at: k) derivativeFont: nil at: 0 ]			ifFalse: [				familyDict removeKey: k ]].		Preferences setDefaultFonts: #(		(setSystemFontTo: 'DejaVu' 10)		(setListFontTo: 'DejaVu' 10)		(setMenuFontTo: 'DejaVu' 10)		(setWindowTitleFontTo: 'DejaVu' 12)		(setBalloonHelpFontTo: 'DejaVu' 8)		(setCodeFontTo: 'DejaVu' 10)		(setButtonFontTo: 'DejaVu' 10))! !!StrikeFont class methodsFor: 'removing' stamp: 'jmv 1/17/2011 00:22'!removeSomeFonts"StrikeFont removeSomeFonts"	| familyDict |	familyDict _ AvailableFonts at: 'DejaVu'.	familyDict keys do: [ :k |		"No boldItalic for the followint"		(#(5 6 7 8 9 10 11 12 14 17 22) includes: k)			ifTrue: [ (familyDict at: k) derivativeFont: nil at: 3 ].		"No derivatives at all for the following"		(#() includes: k)			ifTrue: [ (familyDict at: k) derivativeFont: nil at: 0 ].		"Sizes to keep"		(#(5 6 7 8 9 10 11 12 14 17 22) includes: k) 			ifFalse: [ familyDict removeKey: k ]].		Preferences setDefaultFonts: #(		(setSystemFontTo: 'DejaVu' 9)		(setListFontTo: 'DejaVu' 9)		(setMenuFontTo: 'DejaVu' 10)		(setWindowTitleFontTo: 'DejaVu' 12)		(setBalloonHelpFontTo: 'DejaVu' 8)		(setCodeFontTo: 'DejaVu' 9)		(setButtonFontTo: 'DejaVu' 9))! !!SystemDictionary methodsFor: 'retrieving' stamp: 'jmv 1/17/2011 00:25'!poolUsers	"Answer a dictionary of pool name -> classes that refer to it. Also includes any globally know dictionaries (such as Smalltalk, Undeclared etc) which although not strictly accurate is potentially useful information "	"Smalltalk poolUsers"	| poolUsers pool refs |	poolUsers _ Dictionary new.	Smalltalk keys		do: [ :k |			 (((pool _ Smalltalk at: k) isKindOf: Dictionary)					or: [pool isKindOf: SharedPool class])				ifTrue: [refs _ Smalltalk allClasses								select: [:c | c sharedPools identityIncludes: pool]								thenCollect: [:c | c name].					refs						add: (Smalltalk								allCallsOn: (Smalltalk associationAt: k)).					poolUsers at: k put: refs]].	^ poolUsers! !!SystemDictionary methodsFor: 'ui' stamp: 'jmv 1/17/2011 00:24'!inspectGlobals	"Smalltalk  inspectGlobals"		| associations aDict |	associations _ ((self  keys select: [:aKey | ((self  at: aKey) isKindOf: Class) not]) collect:[:aKey | self associationAt: aKey]).	aDict _ IdentityDictionary new.	associations do: [:as | aDict add: as].	aDict inspectWithLabel: 'The Globals'! !!TextStyle class methodsFor: 'instance accessing' stamp: 'jmv 1/16/2011 23:55'!availableTextStyleNames	"Answer the names of the known text styles, sorted in alphabetical order"	"	TextStyle availableTextStyleNames	"	^ AvailableTextStyles keys sort! !!TheWorldMenu methodsFor: 'construction' stamp: 'jmv 1/16/2011 23:56'!newMorph	"The user requested 'new morph' from the world menu.  Put up a menu that allows many ways of obtaining new morphs.  If the preference #classicNewMorphMenu is true, the full form of yore is used; otherwise, a much shortened form is used."	| menu subMenu catDict shortCat class |	menu _ self menu: 'Add a new morph'.	menu				add: 'from paste buffer' translated		target: myHand		action: #pasteMorph;				add: 'from alphabetical list' translated		subMenu: self alphabeticalMorphMenu.	menu addLine.	Preferences classicNewMorphMenu ifTrue: [		menu addLine.		catDict _ Dictionary new.		SystemOrganization categories do: [ :cat |			((cat beginsWith: 'Morphic-') and: [ (#('Morphic-Menus' 'Morphic-Support' ) includes: cat) not ]) ifTrue: [				shortCat _ (cat					copyFrom: 'Morphic-' size + 1					to: cat size) translated.				(SystemOrganization listAtCategoryNamed: cat) do: [ :cName |					class _ Smalltalk at: cName.					((class inheritsFrom: Morph) and: [ class includeInNewMorphMenu ]) ifTrue: [						(catDict includesKey: shortCat)							ifTrue: [ (catDict at: shortCat) addLast: class ]							ifFalse: [								catDict									at: shortCat									put: (OrderedCollection with: class) ]]]]].		catDict keys sort do: [ :categ |			subMenu _ MenuMorph new.			((catDict at: categ) asArray sort: [ :c1 :c2 |				c1 name < c2 name ]) do: [ :cl |				subMenu					add: cl name					target: self					selector: #newMorphOfClass:event:					argument: cl ].			menu				add: categ				subMenu: subMenu ]].	self doPopUp: menu.! !Utilities class removeSelector: #inspectGlobals!IdentityDictionary removeSelector: #fasterKeys!