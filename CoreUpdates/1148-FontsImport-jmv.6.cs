'From Cuis 4.0 of 16 November 2011 [latest update: #1144] on 22 November 2011 at 10:20:21 am'!!InnerTextMorph methodsFor: 'drawing' stamp: 'jmv 11/14/2011 17:57'!                           drawOn: aCanvas	"Draw the receiver on a canvas"	false ifTrue: [ self debugDrawLineRectsOn: aCanvas ].  "show line rects for debugging"	aCanvas paragraph: self paragraph bounds: self bounds color: color.	"Drawing the paragraph might change the #lastCaretRect, and therefore might	require a second update."	paragraph lastCaretRectNeedsRedraw ifTrue: [		paragraph lastCaretRect ifNotNil: [ :r | self invalidRect: r ]]! !!InnerTextMorph methodsFor: 'blinking cursor' stamp: 'jmv 11/9/2011 17:57'!       onBlinkCursor	"Blink the cursor"	paragraph ifNil: [ ^nil ].	paragraph showCaret: paragraph showCaret not | pauseBlinking.	pauseBlinking _ false.	paragraph lastCaretRect ifNotNil: [ :r | self invalidRect: r].! !!InnerTextMorph methodsFor: 'blinking cursor' stamp: 'jmv 11/22/2011 09:52'!                          pauseBlinking	"Show a solid cursor (non blinking) for a short while"	pauseBlinking _ true.	paragraph ifNotNil: [		paragraph lastCaretRect ifNotNil: [ :r | self invalidRect: r ].		"Show cursor right now if needed"		paragraph showCaret ifFalse: [			paragraph showCaret: true ]]! !!OneLineEditorMorph methodsFor: 'drawing' stamp: 'jmv 11/21/2011 23:52'!                       displayInsertionMarkAtX: x top: top bottom: bottom emphasis: emphasis on: aCanvas	| caretColor x1 isBold isItalic x0 h w halfW r d |	isBold _ emphasis allMask: 1.	isItalic _ emphasis allMask: 2.	caretColor _ Theme current insertionPoint.	h _ bottom - top.	w _ isBold		ifTrue: [ h // 25 + 2 ]		ifFalse: [ h // 30 + 1 ].	halfW _ w // 2.	isItalic		ifTrue: [				"Keep tweaking if needed!!"			d _ isBold ifTrue: [ 3 ] ifFalse: [ h // 24].			x0 _ x- (h*5//24) + d.			x1 _ x + d ]		ifFalse: [			x0 _ x.			x1 _ x].	x0 < halfW ifTrue: [		x1 _ x1 - x0 + halfW.		x0 _ halfW ].	r _ bounds right-halfW-1.	r < x1 ifTrue: [		x0 _ x0 + r - x1.		x1 _ r ].	caretRect _ x0-halfW@ top corner: x1+halfW+1 @ bottom.	aCanvas		line: x0@(bottom-halfW) to: x1@(top+halfW)		width: w color: caretColor! !!Paragraph methodsFor: 'display' stamp: 'jmv 11/21/2011 16:28'!                        displayInsertionMarkAtX: x top: top bottom: bottom emphasis: emphasis on: aCanvas paragraphTopLeft: paragraphTopLeft	| caretColor x1 isBold isItalic x0 h w halfW r newCaretRect d |	isBold _ emphasis allMask: 1.	isItalic _ emphasis allMask: 2.	caretColor _ Theme current insertionPoint.	h _ bottom - top.	w _ isBold		ifTrue: [ h // 25 + 2 ]		ifFalse: [ h // 30 + 1 ].	halfW _ w // 2.	isItalic		ifTrue: [				"Keep tweaking if needed!!"			d _ isBold ifTrue: [ 3 ] ifFalse: [ h // 24].			x0 _ x- (h*5//24) + d.			x1 _ x + d ]		ifFalse: [			x0 _ x.			x1 _ x].	x0-paragraphTopLeft x < halfW ifTrue: [		x1 _ x1 - x0 + halfW+paragraphTopLeft x.		x0 _ halfW+paragraphTopLeft x ].	r _ extentForComposing x-halfW-1.	r < (x1-paragraphTopLeft x) ifTrue: [		x0 _ x0 + r - x1+paragraphTopLeft x.		x1 _ r +paragraphTopLeft x].	lastCaretRectNeedsRedraw _ false.	newCaretRect _ x0-halfW@ top corner: x1+halfW+1 @ (bottom+1).	lastCaretRect ifNotNil: [		lastCaretRect = newCaretRect ifFalse: [			"If we are actually drawing the last position of the text cursor,			but not the current one, request redraw."			((aCanvas isVisible: lastCaretRect) and: [(aCanvas isFullyVisible: newCaretRect) not ])				ifTrue: [					lastCaretRectNeedsRedraw _ true ]]].	lastCaretRect _ newCaretRect.	aCanvas		line: x0@(bottom-halfW) to: x1@(top+halfW)		width: w color: caretColor! !!PopUpMenu methodsFor: 'basic control sequence' stamp: 'jmv 11/22/2011 09:08'!        startUpWithoutKeyboard	"Display and make a selection from the receiver as long as the button  is pressed. Answer the current selection.  Do not allow keyboard input into the menu"		^ self startUpWithCaption: nil at: ActiveHand position allowKeyboard: false! !!StrikeFont methodsFor: 'accessing' stamp: 'jmv 11/22/2011 09:14'!                    baseKern	"Return the base kern value to be used for all characters.	What follows is some 'random' text used to visually adjust this method.	HaHbHcHdHeHfHgHhHiHjHkHlHmHnHoHpHqHrHsHtHuHvHwHxHyHzH	HAHBHCHDHEHFHGHHHIHJHKHLHMHNHOHPHQHRHSHTHUHVHWHXHYHXZH	wok yuyo	wuwu	vuvu	rucu	tucu	WUWU	VUVU	huevo	HUEVO	to											k y mate	runico ridiculo	ARABICO	AAAAA	TOMATE	TUTU	tatadalajafua	abacadafagahaqawaearatayauaiaoapasadafagahajakalazaxacavabanama	kUxUxa	q?d?h?l?t?f?j?"		| italic baseKern |	italic _ emphasis allMask: 2.		"Assume synthetic will not affect kerning (i.e. synthetic italics are not used)"	self familyName = 'DejaVu'		ifTrue: [			baseKern _ (italic or: [ pointSize < 9 ])				ifTrue: [ 1 ]				ifFalse: [ 0 ].			(italic not and: [pointSize = 12]) ifTrue: [				baseKern _ baseKern -1 ].			pointSize >= 13 ifTrue: [				baseKern _ baseKern -1 ].			pointSize >= 20 ifTrue: [				baseKern _ baseKern -1 ]]		ifFalse: [			baseKern _ pointSize < 12				ifTrue: [ 1 ]				ifFalse: [ 0 ].			italic ifTrue: [				baseKern _ baseKern + 1]].		"If synthetic italic"	"See makeItalicGlyphs"	(self isSynthetic and: [ emphasis = 3 ]) ifTrue: [		baseKern _ baseKern + ((self height-1-self ascent+4)//4 max: 0)  		+ (((self ascent-5+4)//4 max: 0)) ].	^baseKern! !!StrikeFont methodsFor: 'accessing' stamp: 'jmv 11/22/2011 09:13'!                      familyName	| lastSpace n |	n _ self name.	lastSpace _ (n findLast: [ :m | m = $  ]).	^ lastSpace > 0		ifTrue: [ n copyFrom: 1 to: lastSpace -1 ]		ifFalse: [ '' ]! !!StrikeFont methodsFor: 'accessing' stamp: 'jmv 11/21/2011 15:46'!                widthOf: aCharacter 	"Answer the width of the argument as a character in the receiver."	| ascii |	ascii _ characterToGlyphMap		ifNil: [ aCharacter asciiValue ]		ifNotNil: [ characterToGlyphMap at: aCharacter asciiValue + 1 ].	(ascii >= minAscii and:[ascii <= maxAscii]) ifFalse: [ascii _ maxAscii + 1].	^ (xTable at: ascii + 2) - (xTable at: ascii + 1)! !!StrikeFont methodsFor: 'displaying' stamp: 'jmv 11/21/2011 15:48'!               widthOfString: aString from: firstIndex to: lastIndex	"Measure the length of the given string between start and stop index"	| resultX |	resultX _ 0.	firstIndex to: lastIndex do:[:i | 		resultX _ resultX + (self widthOf: (aString at: i))].	^ resultX! !!StrikeFont class methodsFor: 'instance creation' stamp: 'jmv 11/22/2011 10:10'!           create: fontName size: pointSize bold: includeBold italic: includeItalic boldItalic: includeBoldItalic	"	self create: 'popo' size: 12 bold: true italic: true boldItalic: true	"	| base bold oblique boldOblique point prefix |	prefix _ 'AAFonts', FileDirectory slash, fontName.	point _ pointSize asString.	base _ [ (StrikeFont new		buildFromForm: (Form fromFileNamed: prefix, '-0-', point, '.bmp')		data: (FileStream oldFileNamed: prefix, '-0-', point, '.txt') contentsOfEntireFile substrings		name: fontName, ' ', point)			pointSize: pointSize ] on: FileDoesNotExistException do: [ nil ].	includeBold ifTrue: [		bold _ [ (StrikeFont new			buildFromForm: (Form fromFileNamed: prefix, '-1-', point, '.bmp')			data: (FileStream oldFileNamed: prefix, '-1-', point, '.txt') contentsOfEntireFile substrings			name: fontName, ' ', point, 'B')				emphasis: 1;				pointSize: pointSize ] on: FileDoesNotExistException do: [ nil ]].	includeItalic ifTrue: [		oblique _ [ (StrikeFont new			buildFromForm: (Form fromFileNamed: prefix, '-2-', point, '.bmp')			data: (FileStream oldFileNamed: prefix, '-2-', point, '.txt') contentsOfEntireFile substrings			name: fontName, ' ', point, 'I')				emphasis: 2;				pointSize: pointSize ] on: FileDoesNotExistException do: [ nil ]].	includeBoldItalic ifTrue: [		boldOblique _ [ (StrikeFont new			buildFromForm: (Form fromFileNamed: prefix, '-3-', point, '.bmp')			data: (FileStream oldFileNamed: prefix, '-3-', point, '.txt') contentsOfEntireFile substrings			name: fontName, ' ', point, 'BI')				emphasis: 3;				pointSize: pointSize ] on: FileDoesNotExistException do: [ nil ]].	"We have a regular, base font. Make others derivatives of it"	base ifNotNil: [		bold ifNotNil: [			base derivativeFont: bold at: 1 ].		oblique ifNotNil: [			base derivativeFont: oblique at: 2].		boldOblique ifNotNil: [			base derivativeFont: boldOblique at: 3 ].		^base ].	"We don't have a base, regular font."	oblique ifNotNil: [		oblique emphasis: 0.	"Hacky. Non regular fonts can not have derivatives. Should change this?"		bold ifNotNil: [			oblique derivativeFont: bold at: 1 ].		boldOblique ifNotNil: [			oblique derivativeFont: boldOblique at: 3 ].		^oblique ].	bold ifNotNil: [		bold emphasis: 0.	"Hacky. Non regular fonts can not have derivatives. Should change this?"		boldOblique ifNotNil: [			bold derivativeFont: boldOblique at: 3 ].		^bold ].	boldOblique ifNotNil: [		^boldOblique ].	^nil! !!StrikeFont class methodsFor: 'instance creation' stamp: 'jmv 11/22/2011 10:18'!          install: aString	"StrikeFont install: '#PilGi'StrikeFont install: 'Optima'StrikeFont install: 'Herculanum'StrikeFont install: 'Papyrus'StrikeFont install: 'Handwriting - Dakota'StrikeFont install: 'Times New Roman'StrikeFont install: 'Apple Chancery'StrikeFont install: 'Cochin'StrikeFont install: 'Cracked'StrikeFont install: 'Zapfino'StrikeFont install: 'Brush Script MT'StrikeFont install: 'Chalkboard'"	| fontDict |	fontDict _ Dictionary new.	"Just try a lot of sizes. Will ignore missing files."	1 to: 200 do: [ :s |		(self create: aString size: s bold: true italic: true boldItalic: true) ifNotNil: [ :font |			fontDict				at: s				put: font ]].	fontDict notEmpty ifTrue: [		AvailableFonts at: aString put: fontDict ].	Preferences restoreDefaultFonts! !!String methodsFor: 'converting' stamp: 'jmv 11/22/2011 08:08'!             withoutLeadingDigits	"Answer the portion of the receiver that follows any leading series of digits and blanks.  If the receiver consists entirely of digits and blanks, return an empty string.	See withoutTrailingDigits"	| firstNonDigit |	firstNonDigit _ (self findFirst: [ :m | m isDigit not and: [ m ~~ $  ]]).	^ firstNonDigit > 0		ifTrue: [ self copyFrom: firstNonDigit  to: self size ]		ifFalse: [ '' ]"'234Whoopie' withoutLeadingDigits' 4321 BlastOff!!' withoutLeadingDigits'wimpy' withoutLeadingDigits'  89Ten 12   ' withoutLeadingDigits'78 92' withoutLeadingDigits'9876 and with several words 9876' withoutLeadingDigits' 123another one123 ' withoutLeadingDigits"! !!String methodsFor: 'converting' stamp: 'jmv 11/22/2011 09:13'!               withoutTrailingDigits	"Answer the portion of the receiver that precedes any trailing series of digits and blanks.  If the receiver consists entirely of digits and blanks, return an empty string.	See #withoutLeadingDigits"	| lastNonDigit |	lastNonDigit _ (self findLast: [ :m | m isDigit not and: [ m ~~ $  ]]).	^ lastNonDigit > 0		ifTrue: [ self copyFrom: 1 to: lastNonDigit ]		ifFalse: [ '' ]"'Whoopie234' withoutTrailingDigits'BlastOff!! 4321 ' withoutTrailingDigits'wimpy' withoutTrailingDigits'  89Ten 12   ' withoutTrailingDigits'78 92' withoutTrailingDigits'and with several words 9876' withoutTrailingDigits' 123another one123 ' withoutTrailingDigits"! !StrikeFont class removeSelector: #installOptima!StrikeFont removeSelector: #fontNameWithPointSize!