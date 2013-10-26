'From Cuis 4.0 of 16 November 2011 [latest update: #1144] on 2 December 2011 at 10:57:12 am'!!Categorizer methodsFor: 'accessing' stamp: 'jmv 12/2/2011 10:45'!                              categories: anArray 	"Reorder my categories to be in order of the argument, anArray. If the 	resulting organization does not include all elements, then give an error."	| newCategories newStops newElements catName list runningTotal | 	newCategories _ Array new: anArray size.	newStops _ Array new: anArray size.	newElements _ #().	runningTotal _ 0.	1 to: anArray size do:		[:i |		catName _ (anArray at: i) asSymbol.		list _ self listAtCategoryNamed: catName.				newElements _ newElements, list.				newCategories at: i put: catName.				newStops at: i put: (runningTotal _ runningTotal + list size)].	elementArray do: [ :element | "check to be sure all elements are included"		(newElements includes: element)			ifFalse: [^self error: 'New categories must match old ones']].	"Everything is good, now update my three arrays."	categoryArray _ newCategories.	categoryStops _ newStops.	elementArray _ newElements! !!MixedSound methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:48'!                     initialize	super initialize.	sounds _ #().	leftVols _ #().	rightVols _ #()! !!BorderedMorph methodsFor: 'accessing' stamp: 'jmv 12/2/2011 10:04'!      borderWidth: anInteger	borderWidth = anInteger ifFalse: [		borderColor ifNil: [ borderColor _ Color black ].		borderWidth _ anInteger max: 0.		self redrawNeeded ]! !!BorderedMorph methodsFor: 'geometry' stamp: 'jmv 12/2/2011 10:23'!                 innerBounds	"Return the inner rectangle enclosed by the bounds of this morph excluding the space taken by its borders. For an unbordered morph, this is just its bounds."	^ bounds insetBy: borderWidth! !!BorderedMorph methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:00'!       initialize	"initialize the state of the receiver"	super initialize.	"initialize the receiver state related to border"	borderColor_ self defaultBorderColor.	borderWidth _ self defaultBorderWidth! !!BorderedMorph methodsFor: 'testing' stamp: 'jmv 12/2/2011 10:25'!                  isOpaqueMorph	"Any submorph that answers true to #isOrthoRectangularMorph (to optimize #containsPoint:)	but is not an opaque rectangle covering bounds MUST answer false to this message"	((color is: #Color) and: [ color isOpaque not ]) ifTrue: [		^false ].	borderWidth > 0 ifTrue: [		((borderColor is: #Color) and: [ borderColor  isOpaque not]) ifTrue: [			^false ]].	^true! !!AutoCompleterMorph methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:09'!               defaultBorderColor	^ Color gray! !!AutoCompleterMorph methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:09'!            defaultBorderWidth	"answer the default border width for the receiver"	^ 1! !!AutoCompleterMorph methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:38'! defaultColor	^ Theme current paneBackgroundFrom: self defaultBorderColor! !!AutoCompleterMorph methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:38'!   initialize 	super initialize.	self startStepping! !!AutoCompleterMorph methodsFor: 'drawing' stamp: 'jmv 12/2/2011 10:24'! drawOn: aCanvas	| rectangle w x0 y0 h y1 y2 |	aCanvas frameAndFillRectangle: self bounds fillColor: self color borderWidth: borderWidth borderColor: borderColor.	x0 _ bounds left+1.	y0 _ bounds top+1.	w _ bounds width-2.	completer entryCount > self class itemsPerPage  ifTrue: [		w _ bounds width-Preferences scrollbarThickness -2.		aCanvas			frameRectangle: (bounds topRight - (Preferences scrollbarThickness@0)				extent: Preferences scrollbarThickness @ bounds height)			borderWidth: 1			color: borderColor.		aCanvas			image: (ScrollbarButton arrowOfDirection: #top)			at: bounds topRight - (Preferences scrollbarThickness@0).		aCanvas			image: (ScrollbarButton arrowOfDirection: #bottom)			at: bounds bottomRight - Preferences scrollbarThickness.		h _ bounds height - (2 * Preferences scrollbarThickness).		y1 _ (1.0 * self firstVisible-1 / completer entryCount * h) ceiling + y0 + Preferences scrollbarThickness-1.		y2 _ (1.0 * self lastVisible / completer entryCount * h) floor + y0 + Preferences scrollbarThickness -1.		aCanvas fillRectangle: (bounds right - Preferences scrollbarThickness+2@y1 corner: bounds right-2 @ y2) colorOrInfiniteForm: Color veryLightGray.		aCanvas		].	self firstVisible		to: self lastVisible		do: [ :index |			rectangle _ x0@y0 extent: w@self class itemHeight.			index = self selected				ifTrue: [					aCanvas fillRectangle: rectangle colorOrInfiniteForm: (Theme current listHighlightFocused: true) ].			aCanvas				drawString: (completer entries at: index) asString				in: rectangle				font: self class listFont				color: Theme current text.			y0 _ y0 + self itemHeight ]! !!AutoCompleterMorph methodsFor: 'as yet unclassified' stamp: 'jmv 12/2/2011 10:24'!                             updateColor	| remaining alpha |	remaining := (self timeout - self timeOfLastActivity).	remaining < 1000 		ifTrue: [			alpha _ remaining / 1000.0.			self color: (self color alpha: alpha).			self borderColor: (borderColor alpha: alpha)			]! !!ImageMorph methodsFor: 'accessing' stamp: 'jmv 12/2/2011 10:12'!   borderWidth: bw	| newExtent |	newExtent _ 2 * bw + image extent.	bounds extent = newExtent ifFalse: [ self basicExtent: newExtent ]! !!LayoutAdjustingMorph methodsFor: 'testing' stamp: 'jmv 12/2/2011 09:59'!          isOpaqueMorph	"Any submorph that answers true to #isOrthoRectangularMorph (to optimize #containsPoint:)	but is not an opaque rectangle covering bounds MUST answer false to this message"	((color is: #Color) and: [ color isOpaque not ]) ifTrue: [		^false ].	^true! !!MagnifierMorph methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:20'!     defaultBorderWidth	"answer the default border width for the receiver"	^ 1! !!MagnifierMorph methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:39'!     initialize	super initialize.	trackPointer _ true.	magnification _ 2.	lastPos _ self sourcePoint.	self extent: 128@128.! !!MenuMorph methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:41'!                       defaultBorderColor	"answer the default border color/fill style for the receiver"	^ #raised! !!MenuMorph methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:41'!                         defaultBorderWidth	"answer the default border width for the receiver"	^ Theme current roundWindowCorners		ifTrue: [0]		ifFalse: [Preferences menuBorderWidth]! !!MenuMorph methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:40'!                    defaultColor	^ Theme current menu! !!MenuMorph methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:41'!                   initialize	super initialize.	bounds _ 0@0 corner: 40@10.	defaultTarget _ nil.	selectedItem _ nil.	stayUp _ false.	popUpOwner _ nil! !!MenuMorph methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:11'!            setTitleParametersFor: aMenuTitle 	aMenuTitle		color: Theme current menuTitleBar;		borderWidth: Preferences menuTitleBorderWidth * 0;		borderColor: #inset! !!PackageInfo methodsFor: 'testing' stamp: 'jmv 12/2/2011 10:47'!                          changeRecordForOverriddenMethod: aMethodReference	| sourceFilesCopy method position |	method := aMethodReference actualClass compiledMethodAt: aMethodReference methodSymbol.	position := method filePosition.	sourceFilesCopy := SourceFiles collect:		[:x | x isNil ifTrue: [ nil ]				ifFalse: [x readOnlyCopy]].	[ | file prevPos prevFileIndex chunk stamp methodCategory tokens |	method fileIndex = 0 ifTrue: [^ nil].	file := sourceFilesCopy at: method fileIndex.	[position notNil & file notNil]		whileTrue:		[file position: (0 max: position-150).  "Skip back to before the preamble"		[file position < (position-1)]  "then pick it up from the front"			whileTrue: [chunk := file nextChunk].		"Preamble is likely a linked method preamble, if we're in			a changes file (not the sources file).  Try to parse it			for prior source position and file index"		prevPos := nil.		stamp := ''.		(chunk findString: 'methodsFor:' startingAt: 1) > 0			ifTrue: [tokens := Scanner new scanTokens: chunk]			ifFalse: [tokens := #()  "ie cant be back ref"].		((tokens size between: 7 and: 8)			and: [(tokens at: tokens size-5) = #methodsFor:])			ifTrue:				[(tokens at: tokens size-3) = #stamp:				ifTrue: ["New format gives change stamp and unified prior pointer"						stamp := tokens at: tokens size-2.						prevPos := tokens last.						prevFileIndex := sourceFilesCopy fileIndexFromSourcePointer: prevPos.						prevPos := sourceFilesCopy filePositionFromSourcePointer: prevPos]				ifFalse: ["Old format gives no stamp; prior pointer in two parts"						prevPos := tokens at: tokens size-2.						prevFileIndex := tokens last].				(prevPos = 0 or: [prevFileIndex = 0]) ifTrue: [prevPos := nil]].		((tokens size between: 5 and: 6)			and: [(tokens at: tokens size-3) = #methodsFor:])			ifTrue:				[(tokens at: tokens size-1) = #stamp:				ifTrue: ["New format gives change stamp and unified prior pointer"						stamp := tokens at: tokens size]].		methodCategory := (tokens after: #methodsFor:) ifNil: ['as yet unclassifed'].		(self includesMethodCategory: methodCategory ofClass: aMethodReference actualClass) ifTrue:			[methodCategory = (Smalltalk at: #Categorizer ifAbsent: [Smalltalk at: #ClassOrganizer]) default ifTrue: [methodCategory := methodCategory, ' '].			^ ChangeRecord new file: file position: position type: #method						class: aMethodReference classSymbol category: methodCategory meta: aMethodReference classIsMeta stamp: stamp].		position := prevPos.		prevPos notNil ifTrue:			[file := sourceFilesCopy at: prevFileIndex]].		^ nil]			ensure: [sourceFilesCopy do: [:x | x notNil ifTrue: [x close]]]	! !!PolygonMorph methodsFor: 'drawing' stamp: 'jmv 12/2/2011 10:26'!       drawBorderOn: aCanvas usingEnds: anArray 	"Display my border on the canvas."	"NOTE: Much of this code is also copied in drawDashedBorderOn:  	(should be factored)"	| bigClipRect p1i p2i |	borderDashSpec		ifNotNil: [^ self drawDashedBorderOn: aCanvas usingEnds: anArray].	bigClipRect _ aCanvas clipRect expandBy: borderWidth + 1 // 2.	self lineSegmentsDo: [ :p1 :p2 | 		p1i _ p1 asIntegerPoint.		p2i _ p2 asIntegerPoint.		(arrows ~= #none and: [closed not])			ifTrue: ["Shorten line ends so as not to interfere with tip of arrow."					((arrows == #back								or: [arrows == #both])							and: [p1 = vertices first])						ifTrue: [p1i _ anArray first asIntegerPoint].					((arrows == #forward								or: [arrows == #both])							and: [p2 = vertices last])						ifTrue: [p2i _ anArray last asIntegerPoint]].		(closed or: ["bigClipRect intersects: (p1i rect: p2i) optimized:"			((p1i min: p2i) max: bigClipRect origin)				<= ((p1i max: p2i) min: bigClipRect corner)])				ifTrue: [					aCanvas line: p1i to: p2i width: borderWidth color: borderColor ]]! !!PolygonMorph methodsFor: 'drawing' stamp: 'jmv 12/2/2011 10:26'!        drawDashedBorderOn: aCanvas usingEnds: anArray 	"Display my border on the canvas. NOTE: mostly copied from  	drawBorderOn:"	| lineColor bigClipRect p1i p2i segmentOffset |	(borderColor isNil 		or: [(borderColor is: #Color) and: [borderColor isTransparent]]) ifTrue: [^self].	lineColor := borderColor.	bigClipRect := aCanvas clipRect expandBy: (borderWidth + 1) // 2.	segmentOffset := self borderDashOffset.	self lineSegmentsDo: 			[:p1 :p2 | 			p1i := p1 asIntegerPoint.			p2i := p2 asIntegerPoint.			(arrows ~= #none and: [closed not]) 				ifTrue: 					["Shorten line ends so as not to interfere with tip  					of arrow."					((arrows == #back or: [arrows == #both]) and: [p1 = vertices first]) 						ifTrue: [p1i := anArray first asIntegerPoint].					((arrows == #forward or: [arrows == #both]) and: [p2 = vertices last]) 						ifTrue: [p2i := anArray last asIntegerPoint]].			(closed or: 					["bigClipRect intersects: (p1i rect: p2i) optimized:"					((p1i min: p2i) max: bigClipRect origin) 						<= ((p1i max: p2i) min: bigClipRect corner)]) 				ifTrue: 					[					segmentOffset := aCanvas 								line: p1i								to: p2i								width: borderWidth								color: lineColor								dashLength: borderDashSpec first								secondColor: borderDashSpec third								secondDashLength: borderDashSpec second								startingOffset: segmentOffset]]! !!PolygonMorph methodsFor: 'drawing' stamp: 'jmv 12/2/2011 10:26'!                   drawOn: aCanvas 	"Display the receiver, a spline curve, approximated by straight line segments."	| lineColor bigClipRect brush p1i p2i |	vertices size < 1 ifTrue: [self error: 'a polygon must have at least one point'].	closed & color isTransparent not ifTrue: [		self filledForm colors: (Array with: Color transparent with: color).		aCanvas image: self filledForm at: bounds topLeft-1 ].	lineColor _ borderColor. 	bigClipRect _ aCanvas clipRect expandBy: borderWidth+1//2.	brush _ nil.	self lineSegmentsDo: [ :p1 :p2 |		p1i _ p1 asIntegerPoint.  p2i _ p2 asIntegerPoint.		(closed or: ["bigClipRect intersects: (p1i rect: p2i) optimized:"					((p1i min: p2i) max: bigClipRect origin) <=					((p1i max: p2i) min: bigClipRect corner)]) ifTrue: [			(borderWidth > 3 and: [borderColor is: #Color])			ifTrue: [brush ifNil: [						brush _ (ColorForm dotOfSize: borderWidth)								colors: (Array with: Color transparent with: borderColor)].					aCanvas line: p1i to: p2i brushForm: brush]			ifFalse: [aCanvas line: p1i to: p2i							width: borderWidth color: lineColor]]].	self arrowForms ifNotNil: [		self arrowForms do: [ :f |			f colors: (Array with: Color transparent with: borderColor).			aCanvas image: f at: f offset]]! !!PolygonMorph methodsFor: 'visual properties' stamp: 'jmv 12/2/2011 10:27'!                   color	^self isOpen		ifTrue: [ borderColor  "easy access to line color from halo"]		ifFalse: [ super color]! !!PolygonMorph methodsFor: 'private' stamp: 'jmv 12/2/2011 10:49'!           arrowForms	"ArrowForms are computed only upon demand"	arrowForms ifNotNil: [^ arrowForms].	arrowForms _ #().	(closed or: [ arrows == #none or: [ vertices size < 2 ]])		ifTrue: [^ arrowForms].	(arrows == #forward or: [ arrows == #both ]) ifTrue: [		arrowForms _ arrowForms copyWith:			(self computeArrowFormAt: vertices last from: self nextToLastPoint)].	(arrows == #back or: [ arrows == #both ]) ifTrue: [		arrowForms _ arrowForms copyWith:			(self computeArrowFormAt: vertices first from: self nextToFirstPoint)].	^ arrowForms! !!ProgressBarMorph methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:53'!                 defaultBorderWidth	"answer the default border width for the receiver"	^ 1! !!ProgressBarMorph methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:52'!   defaultColor	^Color white! !!ProgressBarMorph methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:53'!                    initialize	super initialize.	progressColor _ Color gray.	value _ 0.0! !!ProgressMorph methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:55'!         defaultBorderColor	"answer the default border color/fill style for the receiver"	^ Color black! !!ProgressMorph methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:54'!                 defaultBorderWidth	"answer the default border width for the receiver"	^ 2! !!ProgressMorph methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:55'!      defaultColor	^Color veryLightGray! !!ProgressMorph methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:53'!               initialize	super initialize.	self separation: 0.	labelMorph _ StringMorph contents: '' font: AbstractFont default.	subLabelMorph _ StringMorph contents: '' font: AbstractFont default.	progress_ ProgressBarMorph new.	progress extent: 200 @ 15.	self addMorph: labelMorph.	self addMorph: subLabelMorph.	self addMorph: progress fixedHeight: 15.! !!ProgressMorph methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:55'!                    setupMorphs	|  w h |	w _ ((labelMorph width max: subLabelMorph width) max: progress width) + 2.	h _ labelMorph height + subLabelMorph height + progress height + 2.	self bounds: (0@0 extent: w@h).	self align: self fullBounds center with: Display boundingBox center! !!SHTextStylerST80 class methodsFor: 'style table' stamp: 'jmv 12/2/2011 10:47'!                             attributeArrayForColor: aColorOrNil emphasis: anEmphasisSymbolOrArrayorNil	"Answer a new Array containing any non nil TextAttributes specified"	| answer emphArray |	answer _ #().	aColorOrNil ifNotNil: [ answer_ answer, {TextColor color: aColorOrNil} ].	anEmphasisSymbolOrArrayorNil ifNotNil: [		emphArray _ anEmphasisSymbolOrArrayorNil isSymbol 			ifTrue: [ {anEmphasisSymbolOrArrayorNil} ] 			ifFalse: [ anEmphasisSymbolOrArrayorNil ].		emphArray do: [ :each |			each ~= #normal				ifTrue: [					answer _ answer, {TextEmphasis perform: each}]]].	^answer! !!ScorePlayer methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:49'!                          initialize	super initialize.	score _ MIDIScore new.	instruments _ #().	overallVolume _ 0.5.	leftVols _ #().	rightVols _ #().	muted _ #().	rate _ 1.0.	repeat _ false.	durationInTicks _ 100! !!ScrollBar methodsFor: 'initialize' stamp: 'jmv 12/2/2011 10:03'!                  initializeSlider	"initialize the receiver's slider"			slider _ RectangleMorph newBounds: self totalSliderArea color: self thumbColor.	sliderShadow := RectangleMorph newBounds: self totalSliderArea						color: self pagingArea color.	slider on: #mouseMove send: #scrollAbsolute: to: self.	slider on: #mouseDown send: #mouseDownInSlider: to: self.	slider on: #mouseUp send: #mouseUpInSlider: to: self.	slider		borderWidth: 1;		borderColor: #raised.	sliderShadow		borderWidth: 1;		borderColor: #inset.	"(the shadow must have the pagingArea as its owner to highlight properly)"	self pagingArea addMorph: sliderShadow.	sliderShadow hide.	self addMorph: slider.	self computeSlider.		self sliderColor: self sliderColor! !!ScrollbarButton methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:36'!                   defaultBorderColor	"answer the default border color/fill style for the receiver"	^ #raised! !!ScrollbarButton methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:35'!                   defaultBorderWidth	"answer the default border width for the receiver"	^ 1! !!SequentialSound methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:49'!   initialize	super initialize.	sounds _ #().	currentIndex _ 0.! !!StarMorph methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:51'!                   initialize"initialize the state of the receiver"	| pt ext oldR points |	super initialize.	pt _ 10 @ 10.	ext _ pt r.	oldR _ ext.	points _ 5.	vertices _ (0 to: 359 by: 360 // points // 2)				collect: [ :angle |					(Point						r:							(oldR _ oldR = ext								ifTrue: [ext * 5 // 12]								ifFalse: [ext])						degrees: angle + pt degrees)							+ (45 @ 45)].	self computeBounds! !!SystemWindow methodsFor: 'geometry' stamp: 'jmv 12/2/2011 10:26'!           innerBounds	"Exclude the label area"	^ bounds insetBy: (borderWidth @ (self labelHeight+borderWidth) corner: borderWidth @ borderWidth)! !!SystemWindow methodsFor: 'geometry' stamp: 'jmv 12/2/2011 10:27'!              titleAreaInnerRect	"Assumes a border will be drawn at the left, top and right of the title area.	The look is that the title area is inside the window"	^ (bounds insetBy: borderWidth) withHeight: self labelHeight! !!SystemWindow methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:43'!                            defaultBorderColor	"answer the default border color/fill style for the receiver"	^ #raised! !!SystemWindow methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:49'!                      initialize	"Initialize a system window. Add label, stripes, etc., if desired"	super initialize.	labelString ifNil: [ labelString _ 'Untitled Window'].	isCollapsed _ false.		self wantsLabel ifTrue: [self initializeLabelArea].	self extent: 300 @ 200.	updatablePanes _ #().	adjusters _ Dictionary new.	adjusters at: #topAdjuster put: WindowEdgeAdjustingMorph top.	adjusters at: #bottomAdjuster put: WindowEdgeAdjustingMorph bottom.	adjusters at: #leftAdjuster put: WindowEdgeAdjustingMorph left.	adjusters at: #rightAdjuster put: WindowEdgeAdjustingMorph right.	adjusters at: #topLeftAdjuster put: WindowEdgeAdjustingMorph topLeft.	adjusters at: #bottomLeftAdjuster put: WindowEdgeAdjustingMorph bottomLeft.	adjusters at: #topRightAdjuster put: WindowEdgeAdjustingMorph topRight.	adjusters at: #bottomRightAdjuster put: WindowEdgeAdjustingMorph bottomRight.	adjusters do: [ :m |		self addMorph: m ].	"by default"	self beColumn! !!SystemWindow methodsFor: 'change reporting' stamp: 'jmv 12/2/2011 10:26'!                             invalidateTitleArea		bounds ifNotNil: [		"not really pretty... also invalidating the top border, regardless of it being above or below the title area		(#titleAreaRect and #titleAreaInnerRect)"		self invalidRect: (bounds withHeight: self labelHeight + borderWidth) ]! !!Taskbar methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:56'!        defaultColor	^ Theme current menu! !!Taskbar methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:56'!                     initialize	super initialize.	self		step;		startStepping! !!WorldState methodsFor: 'initialization' stamp: 'jmv 12/2/2011 10:49'!                      initialize	hands _ #().	damageRecorder_ DamageRecorder new.	stepList _ Heap sortBlock: self stepListSortBlock.	lastStepTime _ 0.	lastAlarmTime _ 0.	drawingFailingMorphs _ IdentitySet new.	pause _ MinCycleLapse! !ScrollbarButton removeSelector: #initialize!ProgressMorph removeSelector: #initLabelMorph!ProgressMorph removeSelector: #initProgressMorph!ProgressMorph removeSelector: #initSubLabelMorph!MenuMorph removeSelector: #setDefaultParameters!MagnifierMorph removeSelector: #defaultColor!AutoCompleterMorph class removeSelector: #borderColor!BorderedMorph removeSelector: #borderInitialize!BorderedMorph removeSelector: #borderWidth!BorderedMorph removeSelector: #setBorderWidth:borderColor:!BorderedMorph removeSelector: #setColor:borderWidth:borderColor:!Morph removeSelector: #borderWidth!