'From Cuis 2.9 of 5 November 2010 [latest update: #634] on 30 November 2010 at 10:27:56 pm'!!CodeHolder methodsFor: 'controls' stamp: 'jmv 11/30/2010 22:22'!optionalButtonTuples	"Answer a tuple buttons, in the format:			button label			selector to send			help message"	| aList |	aList _ #(	(10	'browse'			browseMethodFull				'view this method in a browser')	(11	'senders' 			browseSendersOfMessages	'browse senders of...')	(16	'implementors'		browseMessages				'browse implementors of...')	(12	'versions'			browseVersions					'browse versions')), 	(Preferences decorateBrowserButtons		ifTrue:			[{#(13	'inheritance'		methodHierarchy 	'browse method inheritancegreen: sends to supertan: has override(s)mauve: both of the abovepink: is an override but doesn''t call superpinkish tan: has override(s), also is an override but doesn''t call super' )}]		ifFalse:			[{#(13	'inheritance'		methodHierarchy	'browse method inheritance')}]),	#(	(12	'hierarchy'			classHierarchy					'browse class hierarchy')	(10	'inst vars'			browseInstVarRefs				'inst var refs...')	(11	'class vars'			browseClassVarRefs			'class var refs...')	(10	'show...'				offerWhatToShowMenu		'menu of what to show in lower pane')).	^ aList! !!Integer class methodsFor: 'UTF-8 conversion' stamp: 'jmv 11/30/2010 18:51'!evaluate: aBlock withUtf8BytesOfUnicodeCodePoint: aCodePoint	"	See senders for typical usage		| characters bytes |	characters _ '����ƥ�' readStream.	bytes _ ByteArray streamContents: [ :strm |		[ characters atEnd ] whileFalse: [			Integer evaluate: [ :byte | strm nextPut: byte ] withUtf8BytesOfUnicodeCodePoint: characters next asciiValue ]].	self assert: bytes hex = 'C3A1C3A5C3A6C3B1C386C2A5C3BC'	"	| mask nBytes shift |	aCodePoint < 128 ifTrue: [		^aBlock value: aCodePoint ].	nBytes _ aCodePoint highBit + 3 // 5.	mask _ #(128 192 224 240 248 252 254 255) at: nBytes.	shift _ nBytes - 1 * -6.	aBlock value: (aCodePoint bitShift: shift) + mask.	2 to: nBytes do: [ :i | 		shift _ shift + 6.		aBlock value: ((aCodePoint bitShift: shift) bitAnd: 63) + 128.	]! !!PluggableButtonMorph methodsFor: 'drawing' stamp: 'jmv 11/30/2010 20:00'!draw3DLookOn: aCanvas 	| w f center x y borderStyleSymbol c availableW l labelMargin |	borderStyleSymbol _ self isPressed ifFalse: [ #raised ] ifTrue: [ #inset ].	c _ self fillStyle asColor.	self mouseIsOver ifTrue: [ c _ c  lighter ].	aCanvas		fillRectangle: bounds		fillStyle: c		borderWidth: 2		borderStyleSymbol: borderStyleSymbol.	f _ self fontToUse.	center _ bounds center.	"	label ifNotNil: [		w _ f widthOfString: label.		x _ bounds width > w			ifTrue: [ center x - (w // 2) ]			ifFalse: [ bounds left +4].		y _ center y - (f height // 2).		self isPressed ifTrue: [			x _ x + 1.			y _ y + 1 ].		aCanvas drawString: label at: x@y font: f color: ColorTheme current buttonLabel ].	"	label ifNotNil: [		labelMargin _ 4.		w _ f widthOfString: label.		availableW _ bounds width-labelMargin-labelMargin-1.		availableW >= w			ifTrue: [				x _ center x - (w // 2).				l _ label ]			ifFalse: [				x _ bounds left + labelMargin.				l _ label squeezedTo: (label size * availableW / w) rounded ].		y _ center y - (f height // 2).		self isPressed ifTrue: [			x _ x + 1.			y _ y + 1 ].		aCanvas			drawString: l			in: (x@y extent: bounds extent - (labelMargin*2-2@4))			font: f			color: ColorTheme current buttonLabel ]! !!PluggableButtonMorph methodsFor: 'drawing' stamp: 'jmv 11/30/2010 20:01'!drawOn: aCanvas	ColorTheme current roundButtons		ifTrue: [ self drawRoundGradientLookOn: aCanvas ]		ifFalse: [ self draw3DLookOn: aCanvas ]! !!PluggableButtonMorph methodsFor: 'drawing' stamp: 'jmv 11/30/2010 22:08'!drawRoundGradientLookOn: aCanvas	| w f center x y r c rect c2 lh height left top width bottomLeftForm bottomRightForm gradientForm topLeftForm topRightForm labelMargin l availableW targetSize |	aCanvas		fillRectangle: bounds		fillStyle: 			"(color adjustSaturation: -0.3 brightness: 0)"			(Color h: color hue s: color saturation * 0.3 v: color brightness "*0+ 0.8").	rect _ bounds insetBy: 2@3.	c _ self fillStyle asColor.	self isPressed		ifFalse: [			self mouseIsOver				ifTrue: [ c _ c adjustSaturation: 0.0 brightness: 0.0 ]				ifFalse: [					"c _ c adjustSaturation: -0.02 brightness: 0.03"					c _ Color h: c hue s: c saturation * 0.5 v: c brightness 					].			c2 _ c * ColorTheme current buttonGradientBottomFactor.			gradientForm _ self class buttonGradient.			topLeftForm _ self class roundedCornerTL.			topRightForm _ self class roundedCornerTR.			bottomLeftForm _ self class roundedCornerBL.			bottomRightForm _ self class roundedCornerBR ]		ifTrue: ["			c _ c adjustSaturation: -0.15 brightness: 0.0."			c _ c adjustSaturation: 0.1 brightness: -0.1.			c2 _ c * ColorTheme current buttonGradientTopFactor.			gradientForm _ self class buttonGradientPressed.			topLeftForm _ self class roundedCornerTLPressed.			topRightForm _ self class roundedCornerTRPressed.			bottomLeftForm _ self class roundedCornerBLPressed.			bottomRightForm _ self class roundedCornerBRPressed ].			lh _ gradientForm form height.	r _ topLeftForm width.	left _ rect left + r.	top _ rect top.	width _ rect width - r-r.	height _ rect height -r min: lh.	aCanvas fillRectangle: (left@top extent: width@height)  infiniteForm: gradientForm multipliedBy: c.	aCanvas image: topLeftForm multipliedBy: c in: (rect topLeft extent: r@height).	aCanvas image: topRightForm multipliedBy: c in: (rect topRight - (r@0)extent: r@height).	aCanvas fillRectangle: (rect origin + (0@height) extent: rect width@(rect height-height-r)) fillStyle: c2.	aCanvas image: bottomLeftForm multipliedBy: c at: rect bottomLeft - (0@r).	aCanvas image: bottomRightForm multipliedBy: c at: rect bottomRight - (r@r) .	aCanvas fillRectangle: (rect bottomLeft+(r@ r negated) corner:  rect bottomRight - (r@0) ) fillStyle: c2.				f _ self fontToUse.	center _ bounds center.	label ifNotNil: [		labelMargin _ 7.		w _ f widthOfString: label.		availableW _ bounds width-labelMargin-labelMargin.		availableW >= w			ifTrue: [				l _ label ]			ifFalse: [				x _ bounds left + labelMargin.				targetSize _ label size * availableW // w.				l _ label squeezedTo: targetSize.				(f widthOfString: l) > availableW ifTrue: [					targetSize _ targetSize - 1.					l _ label squeezedTo: targetSize ]].				w _ f widthOfString: l.		x _ center x - (w // 2).		y _ center y - (f height // 2).		aCanvas			drawStringEmbossed: l			in: (x@y extent: bounds extent - (labelMargin*2-2@4))			font: f			color: ColorTheme current buttonLabel ]! !!String methodsFor: 'converting' stamp: 'jmv 11/30/2010 19:39'!asCamelCase	"Answer a new String, without any whitespace, and with words capitalized (Except for the first one)	' how do you do? ' asCamelCase	"	^ String streamContents: [ :outStream | | inStream capitalize wroteSome |		wroteSome _ false.		capitalize _ false.		inStream _ self readStream.		[ inStream atEnd ] whileFalse: [ | c |			c _ inStream next.			c isSeparator				ifTrue: [ capitalize _ true ]				ifFalse: [					capitalize & wroteSome ifTrue: [ c _ c asUppercase ].					outStream nextPut: c.					wroteSome _ true.					capitalize _ false ]]]! !!String methodsFor: 'converting' stamp: 'jmv 11/30/2010 20:01'!squeezedTo: n	"Examples:	Do nothing:		'This one is a rather long phrase' squeezedTo: 32	1-remove blanks (result can be shorter than asked):		'This one is a rather long phrase' squeezedTo: 30	2-remove necessary trailing vowels		'This one is a rather long phrase' squeezedTo: 24	3-truncate as needed (and add ellipsis)		'This one is a rather long phrase' squeezedTo: 15	4-avoid ellipsis		'This one is a rather long phrase' squeezedTo: 5	"	| vowelCount read write i char allowedVowels str desiredSize postFix j |	str := self.	desiredSize := n.	str size <= n ifTrue: [^str].	str := str asCamelCase.	str size <= n ifTrue: [^str].	postFix := ''.	desiredSize := n - postFix size.	vowelCount := str		inject: 0		into: [:prev :each | each isVowel ifTrue: [prev + 1] ifFalse: [prev]].	str size - vowelCount <= desiredSize		ifTrue: [allowedVowels := vowelCount - (str size - desiredSize)]		ifFalse: [			allowedVowels := 0.			postFix := '...'.			n - postFix size < 5 ifTrue: [postFix := ''].			desiredSize := n - postFix size].	read := str readStream.	write := '' writeStream.	i := 0.	j := 0.	[read atEnd not and: [j < desiredSize]] whileTrue: [		char := read next.		(char isVowel not or: [i < allowedVowels]) ifTrue: [			char isVowel ifTrue: [i := i + 1].			write nextPut: char.			j := j + 1]].	str := write contents , postFix.	^ str! !!VersionsBrowser methodsFor: 'init & update' stamp: 'jmv 11/30/2010 22:15'!buttonSpecs	^#(		(22		'compare to current'		compareToCurrentVersion		'opens a separate window which shows the text differences between the selected version and the current version')		(9		'revert'		fileInSelections		'reverts the method to the version selected')		(24		'remove from changes'		removeMethodFromChanges		'remove this method from the current change set')		(7		'help'		offerVersionsHelp		'further explanation about use of Versions browsers')	)! !