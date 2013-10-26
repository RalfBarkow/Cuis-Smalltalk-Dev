'From Cuis 1.0 of 6 March 2009 [latest update: #5989] on 24 March 2009 at 2:35:09 pm'!!BitBltSimulation methodsFor: 'inner loop' stamp: 'jmv 3/23/2009 20:34'!copyLoopNoSource	"Faster copyLoop when source not used. hDir and vDir are both  	positive, and perload and skew are unused"		| halftoneWord mergeWord mergeFnwith destWord destIndexLocal nWordsMinusOne |	self inline: false.	self var: #mergeFnwith declareC: 'int (*mergeFnwith)(int, int)'.	mergeFnwith _ self		cCoerce: (opTable at: combinationRule + 1)		to: 'int (*)(int, int)'.	mergeFnwith.	"null ref for compiler"	destIndexLocal _ destIndex.	nWordsMinusOne _ nWords - 1.	1		to: bbH		do: [:i | 			"here is the vertical loop"			noHalftone				ifTrue: [halftoneWord _ AllOnes]				ifFalse: [halftoneWord _ self halftoneAt: dy + i - 1].			"Note: the horizontal loop has been expanded into three parts for 			speed:"			"This first section requires masking of the destination store..."			destMask _ mask1.			destWord _ self dstLongAt: destIndexLocal.			mergeWord _ self mergeFn: halftoneWord with: destWord.			destWord _ (destMask bitAnd: mergeWord)						bitOr: (destWord bitAnd: destMask bitInvert32).			self dstLongAt: destIndexLocal put: destWord.			destIndexLocal _ destIndexLocal + 4.			"This central horizontal loop requires no store masking"			destMask _ AllOnes.			combinationRule = 3				ifTrue: ["Special inner loop for STORE"					destWord _ halftoneWord.					2						to: nWordsMinusOne						do: [:word | 							self dstLongAt: destIndexLocal put: destWord.							destIndexLocal _ destIndexLocal + 4]]				ifFalse: ["Normal inner loop does merge"					2						to: nWordsMinusOne						do: [:word | 							"Normal inner loop does merge"							destWord _ self dstLongAt: destIndexLocal.							mergeWord _ self mergeFn: halftoneWord with: destWord.							self dstLongAt: destIndexLocal put: mergeWord.							destIndexLocal _ destIndexLocal + 4]].			"This last section, if used, requires masking of the destination store..."			nWords > 1				ifTrue: [destMask _ mask2.					destWord _ self dstLongAt: destIndexLocal.					mergeWord _ self mergeFn: halftoneWord with: destWord.					destWord _ (destMask bitAnd: mergeWord)								bitOr: (destWord bitAnd: destMask bitInvert32).					self dstLongAt: destIndexLocal put: destWord.					destIndexLocal _ destIndexLocal + 4].			destIndexLocal _ destIndexLocal + destDelta ]! !!Browser methodsFor: 'class functions' stamp: 'jmv 3/23/2009 20:31'!copyClass	| originalName copysName class oldDefinition newDefinition |		classListIndex = 0 ifTrue: [^ self].	self okToChange ifFalse: [^ self].	originalName _ self selectedClass name.	copysName _ self request: 'Please type new class name' initialAnswer: originalName.	copysName = '' ifTrue: [^ self].  " Cancel returns '' "	copysName _ copysName asSymbol.	copysName = originalName ifTrue: [^ self].	(Smalltalk includesKey: copysName)		ifTrue: [^ self error: copysName , ' already exists'].	oldDefinition _ self selectedClass definition.	newDefinition _ oldDefinition copyReplaceAll: '#' , originalName asString with: '#' , copysName asString.	Cursor wait 		showWhile: [			class _ Compiler evaluate: newDefinition logged: true.			class copyAllCategoriesFrom: (Smalltalk at: originalName).			class class copyAllCategoriesFrom: (Smalltalk at: originalName) class ].	self classListIndex: 0.	self changed: #classList! !!CCodeGenerator methodsFor: 'C translation' stamp: 'jmv 3/23/2009 20:18'!generatePreDecrement: msgNode on: aStream indent: level	"Generate the C code for this message onto the given stream."	| varNode |	varNode _ msgNode receiver.	varNode isVariable ifFalse: [ 		self error: 'preDecrement can only be applied to variables' ].	aStream nextPutAll: '--'.	aStream nextPutAll: (self returnPrefixFromVariable: varNode name)! !!ChangeList class methodsFor: 'public access' stamp: 'jmv 3/23/2009 20:19'!browseRecent: charCount on: origChangesFile 	"Opens a changeList on the end of the specified changes log file"		| changeList end changesFile |	changesFile _ origChangesFile readOnlyCopy.	end _ changesFile size.	Cursor read showWhile: [		changeList _ self new			scanFile: changesFile			from: (0 max: end - charCount)			to: end ].	changesFile close.	self		open: changeList		name: 'Recent changes'		multiSelect: true! !!Float methodsFor: 'arithmetic' stamp: 'jmv 3/23/2009 20:33'!/ aNumber 	"Primitive. Answer the result of dividing receiver by aNumber.	Fail if the argument is not a Float.	Essential. See Object clas >> whatIsAPrimitive."	<primitive: 50>	aNumber isZero ifTrue: [^(ZeroDivide dividend: self) signal].	^ aNumber adaptToFloat: self andSend: #/! !!Integer methodsFor: 'benchmarks' stamp: 'jmv 3/23/2009 20:30'!tinyBenchmarks	"Report the results of running the two tiny Squeak benchmarks.	ar 9/10/1999: Adjusted to run at least 1 sec to get more stable results"	"0 tinyBenchmarks"	"On a 292 MHz G3 Mac: 22727272 bytecodes/sec; 984169 sends/sec"	"On a 400 MHz PII/Win98:  18028169 bytecodes/sec; 1081272 sends/sec"	| t1 t2 r n1 n2 |	n1 _ 1.	[		t1 _ Time millisecondsToRun: [n1 benchmark].		t1 < 1000] 			whileTrue:[n1 _ n1 * 2]. "Note: #benchmark's runtime is about O(n)"	n2 _ 28.	[		t2 _ Time millisecondsToRun: [r _ n2 benchFib].		t2 < 1000] 			whileTrue:[n2 _ n2 + 1]. 	"Note: #benchFib's runtime is about O(k^n),		where k is the golden number = (1 + 5 sqrt) / 2 = 1.618...."	^ ((n1 * 500000 * 1000) // t1) printString, ' bytecodes/sec; ',	  ((r * 1000) // t2) printString, ' sends/sec'! !!MessageSet methodsFor: 'private' stamp: 'jmv 3/23/2009 20:32'!initializeMessageList: anArray	| s |	messageList _ OrderedCollection new.	anArray do: [ :each |		MessageSet 			parse: each  			toClassAndSelector: [ :class :sel |				class ifNotNil: [					s _ class name , ' ' , sel , ' {' , ((class organization categoryOfElement: sel) ifNil: ['']) , '}'.					messageList add: (						MethodReference new							setClass: class  							methodSymbol: sel 							stringVersion: s) ]]].	messageListIndex _ messageList isEmpty ifTrue: [0 ] ifFalse: [1].	contents _ ''! !!PositionableStream methodsFor: 'positioning' stamp: 'jmv 3/23/2009 20:26'!skip: n 	"Skips the next amount objects in the receiver's future sequence values."	self position: (self position + (n min: (self contents size - self position)))! !!SequenceableCollection methodsFor: 'converting' stamp: 'jmv 3/23/2009 20:29'!asStringWithCr	"Convert to a string with returns between items.  Elements are usually strings.	 Useful for labels for PopUpMenus.	#('something' 'there') asStringWithCr	"		^String streamContents: [ :labelStream |		self do: [ :each |			(each isKindOf: String)				ifTrue: [ labelStream nextPutAll: each; cr ]				ifFalse: [					each printOn: labelStream.					labelStream cr ]].		self size > 0 ifTrue: [ labelStream skip: -1 ]]! !!String methodsFor: 'internet' stamp: 'jmv 3/23/2009 20:22'!isoToUtf8	"Convert ISO 8559-1 to UTF-8"	"	'Hi there' isoToUtf8	"		| v |	^String streamContents: [ :s |		self do: [:c |			v _ c asciiValue.			(v > 128)				ifFalse: [s nextPut: c]				ifTrue: [					s nextPut: (192+(v >> 6)) asCharacter.					s nextPut: (128+(v bitAnd: 63)) asCharacter]]]! !!TAssignmentNode methodsFor: 'inlining' stamp: 'jmv 3/23/2009 20:35'!bindVariableUsesIn: aDictionary	"Do NOT bind the variable on the left-hand-side of an assignment statement."	"was bindVariablesIn:"	expression _ expression bindVariableUsesIn: aDictionary! !String removeSelector: #indexOfSubCollection:!String removeSelector: #subStrings!String removeSelector: #subStrings:!PopUpMenu removeSelector: #startUpSegmented:withCaption:at:!InterpreterPlugin class removeSelector: #isCPP!InterpreterPlugin class removeSelector: #moduleExtension!