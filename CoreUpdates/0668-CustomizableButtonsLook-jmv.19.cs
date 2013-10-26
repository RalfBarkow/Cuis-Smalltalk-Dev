'From Cuis 2.9 of 5 November 2010 [latest update: #634] on 30 November 2010 at 12:24:57 pm'!!classDefinition: #PluggableButtonMorph category: #'Morphic-Windows'!PluggableMorph subclass: #PluggableButtonMorph	instanceVariableNames: 'label font getStateSelector actionSelector getLabelSelector getMenuSelector arguments argumentsProvider argumentsSelector isPressed mouseIsOver '	classVariableNames: 'RoundedCornerBR RoundedCornerBRPressed RoundedCornerTL RoundedCornerTLPressed ButtonGradient RoundedCornerTR RoundedCornerBLPressed ButtonGradientPressed RoundedCornerBL RoundedCornerTRPressed '	poolDictionaries: ''	category: 'Morphic-Windows'!!CodeHolder methodsFor: 'annotation' stamp: 'jmv 11/30/2010 11:46'!defaultButtonPaneHeight	"Answer the user's preferred default height for new button panes."	^Preferences standardButtonFont height * 14 // 8! !!Browser methodsFor: 'initialize-release' stamp: 'jmv 11/30/2010 11:45'!morphicClassColumn	| column switchHeight divider |	column _ AlignmentMorph proportional.	switchHeight _ StrikeFont default height *2-4.	column 		addMorph: self buildMorphicSwitches		fullFrame: (LayoutFrame fractions: (0 @ 1 corner: 1 @ 1)				offsets: (0 @ (1 - switchHeight) corner: 0 @ 0)).	divider _ BorderedSubpaneDividerMorph forTopEdge.	column addMorph: divider		fullFrame: (LayoutFrame fractions: (0 @ 1 corner: 1 @ 1)				offsets: (0 @ switchHeight negated corner: 0 @ (1 - switchHeight))).	column addMorph: self buildMorphicClassList 		fullFrame: (LayoutFrame fractions: (0 @ 0 corner: 1 @ 1)				offsets: (0 @ 0 corner: 0 @ switchHeight negated)).	^column! !!ColorTheme methodsFor: 'menu colors' stamp: 'jmv 11/28/2010 08:04'!menuTitleBar	Display depth = 1 ifTrue: [^ Color white].	Display depth = 2 ifTrue: [^ Color gray].	^ self menu darker! !!ColorTheme methodsFor: 'other options' stamp: 'jmv 11/30/2010 12:05'!buttonGradientBottomFactor	"Will only be used for color themes that answer true to #roundButtons"	^0.92! !!ColorTheme methodsFor: 'other options' stamp: 'jmv 11/30/2010 11:43'!buttonGradientHeight	"Only effective if #roundButtons answers true.	Provide a reasonable default for subclasses."	^14! !!ColorTheme methodsFor: 'other options' stamp: 'jmv 11/30/2010 11:31'!buttonGradientTopFactor	"Will only be used for color themes that answer true to #roundButtons"	^1.0! !!ColorTheme methodsFor: 'other options' stamp: 'jmv 11/30/2010 10:50'!roundButtons	^false! !!ColorTheme methodsFor: 'other options' stamp: 'jmv 11/30/2010 11:35'!roundedButtonRadius	"Only effective if #roundButtons answers true.	Provide a reasonable default for subclasses."	^8! !!ColorTheme methodsFor: 'other options' stamp: 'jmv 11/30/2010 10:46'!roundedWindowRadius	"Only effective if #roundWindowCorners answers true.	Provide a reasonable default for subclasses."	^7! !!ColorTheme methodsFor: 'other options' stamp: 'jmv 11/30/2010 10:59'!useButtonGradient	^false! !!ColorTheme class methodsFor: 'colors' stamp: 'jmv 11/30/2010 10:45'!beCurrent	CurrentPalette := self basicNew initialize.	SHTextStylerST80 initialize.	World color: ColorTheme current background.	ThreePhaseButtonMorph initialize.	SystemWindow initialize.	PluggableButtonMorph initialize.	^ CurrentPalette! !!DarkBluesPalette methodsFor: 'other options' stamp: 'jmv 11/30/2010 10:50'!roundButtons	^true! !!DarkBluesPalette methodsFor: 'other options' stamp: 'jmv 11/30/2010 10:59'!useButtonGradient	^true! !!DarkColorTheme methodsFor: 'other options' stamp: 'jmv 11/30/2010 10:50'!roundButtons	^true! !!DarkColorTheme methodsFor: 'other options' stamp: 'jmv 11/30/2010 10:59'!useButtonGradient	^true! !!FileList class methodsFor: 'instance creation' stamp: 'jmv 11/30/2010 11:47'!defaultButtonPaneHeight	"Answer the user's preferred default height for new button panes."	^Preferences standardButtonFont height * 14 // 8! !!Form class methodsFor: 'creation - anti aliased' stamp: 'jmv 11/30/2010 11:03'!bottomLeftCorner: r height: height gradientTop: gradientTopFactor gradientBottom: gradientBottomFactor	"Create and answer a Form for the bottomLeft corner of a rounded rectangle"	| f bw topColor bottomColor l d c w |	topColor _ Color white * gradientTopFactor.	bottomColor _ Color white * gradientBottomFactor.	w _ r.	bw _ 1.3.	f _ Form		extent: w @ w		depth: 32.	0		to: w - 1		do: [ :y |			c _ bottomColor				mixed: 1.0 * y / (height - 1)				with: topColor.			0				to: w - 1				do: [ :x |					d _ (w - 1 - x @ y) r.					l _ ((r - d max: 0.0) min: bw) / bw.					f						colorAt: x @ y						put: (c alpha: l) ]].	^ f! !!Form class methodsFor: 'creation - anti aliased' stamp: 'jmv 11/30/2010 11:02'!bottomRightCorner: r height: height gradientTop: gradientTopFactor gradientBottom: gradientBottomFactor	"Create and answer a Form for the bottomRight corner of a rounded rectangle"	| f bw topColor bottomColor l d c w |	topColor _ Color white * gradientTopFactor.	bottomColor _ Color white * gradientBottomFactor.	w _ r.	bw _ 1.3.	f _ Form		extent: w @ w		depth: 32.	0		to: w - 1		do: [ :y |			c _ bottomColor				mixed: 1.0 * y / (height - 1)				with: topColor.			0				to: w - 1				do: [ :x |					d _ (x @ y) r.					l _ ((r - d max: 0.0) min: bw) / bw.					f						colorAt: x @ y						put: (c alpha: l) ]].	^ f! !!Form class methodsFor: 'creation - anti aliased' stamp: 'jmv 11/30/2010 10:34'!topLeftCorner: r height: height gradientTop: gradientTopFactor gradientBottom: gradientBottomFactor	"Create and answer a Form with a vertical gray gradient as specified for the topLeft corner of a rounded rectangle"	| f bw topColor bottomColor l d c w |	topColor _ Color white * gradientTopFactor.	bottomColor _ Color white * gradientBottomFactor.	w _ r.	bw _ 1.3.	f _ Form		extent: w @ height		depth: 32.	0		to: height - 1		do: [ :y |			c _ bottomColor				mixed: 1.0 * y / (height - 1)				with: topColor.			0				to: w - 1				do: [ :x |					l _ 1.0.					y < r ifTrue: [						d _ (w - 1 - x @ (w - 1 - y)) r.						l _ ((r - d max: 0.0) min: bw) / bw ].					f						colorAt: x @ y						put: (c alpha: l) ]].	^ f! !!Form class methodsFor: 'creation - anti aliased' stamp: 'jmv 11/30/2010 10:36'!topRightCorner: r height: height gradientTop: gradientTopFactor gradientBottom: gradientBottomFactor	"Create and answer a Form with a vertical gray gradient as specified for the topRight corner of a rounded rectangle"	| f bw topColor bottomColor l d c w |	topColor _ Color white * gradientTopFactor.	bottomColor _ Color white * gradientBottomFactor.	w _ r.	bw _ 1.3.	f _ Form		extent: w @ height		depth: 32.	0		to: height - 1		do: [ :y |			c _ bottomColor				mixed: 1.0 * y / (height - 1)				with: topColor.			0				to: w - 1				do: [ :x |					l _ 1.0.					y < r ifTrue: [						d _ (x @ (w - y - 1)) r.						l _ ((r - d max: 0.0) min: bw) / bw ].					f						colorAt: x @ y						put: (c alpha: l) ]].	^ f! !!FormCanvas methodsFor: 'drawing-images' stamp: 'jmv 11/30/2010 09:56'!image: aForm multipliedBy: aColor at: aPoint	"Multiply aForm and aColor, then blend over destination.	aForm is a kind of advanced stencil, supplying brightness and opacity at each pixel	Display getCanvas image: (SystemWindow roundedCornerTR: 20)multipliedBy: Color red at: 20@20	"	self buildAuxWith: aForm multipliedWith: aColor.	self translucentImage: auxForm at: aPoint sourceRect: aForm boundingBox! !!FormCanvas methodsFor: 'drawing-images' stamp: 'jmv 11/30/2010 10:03'!image: aForm multipliedBy: aColor in: aRectangle	"Multiply aForm and aColor, then blend over destination.	aForm is a kind of advanced stencil, supplying brightness and opacity at each pixel	Display getCanvas image: (SystemWindow roundedCornerTR: 20)multipliedBy: Color red at: 20@20	"	| h w |	self buildAuxWith: aForm multipliedWith: aColor.	w _ aForm width min: aRectangle width.	h _ aForm height min: aRectangle height.	self translucentImage: auxForm at: aRectangle topLeft sourceRect: (0@0 extent: w@h)! !!FormCanvas methodsFor: 'drawing-rectangles' stamp: 'jmv 11/30/2010 09:48'!fillRectangle: aRectangle fillStyle: aFillStyle	"Fill the given rectangle."	| f |	self shadowColor ifNotNil: [		^self fillRectangle: aRectangle color: aFillStyle asColor ].	(aFillStyle isKindOf: InfiniteForm) ifTrue: [		f _ aFillStyle form.		^self fillRectangle: aRectangle tilingWith: f sourceRect: f boundingBox rule: Form paint ].	(aFillStyle isSolidFill) ifTrue: [		^self fillRectangle: aRectangle color: aFillStyle asColor].	self fillRectangle: aRectangle color: aFillStyle asColor! !!FormCanvas methodsFor: 'drawing-rectangles' stamp: 'jmv 11/30/2010 09:52'!fillRectangle: aRectangle infiniteForm: anInfiniteForm multipliedBy: aColor	"Fill aRectangle with the equivalent of anInfiniteForm multiplied by aColor	aForm is a kind of advanced stencil, supplying brightness and opacity at each pixel	Similar to #image:multipliedBy:at:	Display getCanvas fillRectangle: (10@10 extent: 100@100) infiniteForm: (SystemWindow titleGradient: 12) multipliedBy: Color red.	"	| f |	f _ anInfiniteForm form.	self buildAuxWith: f multipliedWith: aColor.	^self fillRectangle: aRectangle tilingWith: auxForm sourceRect: f boundingBox rule: Form paint! !!FormCanvas methodsFor: 'drawing-rectangles' stamp: 'jmv 11/30/2010 09:48'!fillRectangle: aRectangle tilingWith: aForm sourceRect: patternBox rule: aCombinationRule	"We assume that aForm is part of an InfiniteForm"	| additionalOffset rInPortTerms clippedPort targetTopLeft clipOffset ex 	targetBox savedMap top left |	"this is a bit of a kludge to get the form to be aligned where I *think* it should be.	something better is needed, but not now"	ex _ patternBox extent.	additionalOffset _ 0@0.	rInPortTerms _ aRectangle translateBy: origin.	clippedPort _ port clippedBy: rInPortTerms.	targetTopLeft _ clippedPort clipRect topLeft truncateTo: ex.	clipOffset _ rInPortTerms topLeft - targetTopLeft.	additionalOffset _ (clipOffset \\ ex) - ex.	"do it iteratively"	targetBox _ clippedPort clipRect.	savedMap _ clippedPort colorMap.	clippedPort sourceForm: aForm;		fillColor: nil;		combinationRule: aCombinationRule;		sourceRect: patternBox;		colorMap: (aForm colormapIfNeededFor: clippedPort destForm).	top _ (targetBox top truncateTo: patternBox height) + additionalOffset y.	left _  (targetBox left truncateTo: patternBox width) + additionalOffset x.	left to: (targetBox right - 1) by: patternBox width do:		[:x | top to: (targetBox bottom - 1) by: patternBox height do:			[:y | clippedPort destOrigin: x@y; copyBits]].	clippedPort colorMap: savedMap! !!FormCanvas methodsFor: 'private' stamp: 'jmv 11/30/2010 09:43'!buildAuxWith: aForm multipliedWith: aColor	| h w r |	w _ aForm width.	h _ aForm height.	auxForm		ifNotNil: [			w _ w max: auxForm width.			h _ h max: auxForm height.			(auxForm width < w or: [ auxForm height < h ]) ifTrue: [ auxForm _ nil ]].	auxForm		ifNil: [			auxForm _ Form extent: w@h depth: 32.			auxBlitter _ BitBlt toForm: auxForm ].		r _ aForm boundingBox.	auxForm fill: r fillColor: aColor.	auxBlitter		sourceForm: aForm;		combinationRule: Form rgbMul;		sourceRect: r;		copyBits.! !!GrayPalette methodsFor: 'other options' stamp: 'jmv 11/30/2010 10:50'!roundButtons	^true! !!GrayPalette methodsFor: 'other options' stamp: 'jmv 11/30/2010 10:59'!useButtonGradient	^true! !!PluggableButtonMorph methodsFor: 'drawing' stamp: 'jmv 11/30/2010 09:20'!draw3DLookOn: aCanvas 	| w f center x y borderStyleSymbol c |	"This paves the road for configurable button look..."	borderStyleSymbol _ self isPressed ifFalse: [ #raised ] ifTrue: [ #inset ].	c _ self fillStyle asColor.	self mouseIsOver ifTrue: [ c _ c  lighter ].	aCanvas		fillRectangle: bounds		fillStyle: c		borderWidth: 2		borderStyleSymbol: borderStyleSymbol.	f _ self fontToUse.	center _ bounds center.	label ifNotNil: [		w _ f widthOfString: label.		x _ bounds width > w			ifTrue: [ center x - (w // 2) ]			ifFalse: [ bounds left +4].		y _ center y - (f height // 2).		self isPressed ifTrue: [			x _ x + 1.			y _ y + 1 ].		aCanvas drawString: label at: x@y font: f color: ColorTheme current buttonLabel ]! !!PluggableButtonMorph methodsFor: 'drawing' stamp: 'jmv 11/30/2010 11:33'!drawOn: aCanvas	ColorTheme current roundButtons		ifTrue: [ self drawRoundGradientLookOn: aCanvas ]		ifFalse: [ self draw3DLookOn: aCanvas ]! !!PluggableButtonMorph methodsFor: 'drawing' stamp: 'jmv 11/30/2010 12:24'!drawRoundGradientLookOn: aCanvas	| w f center x y r c rect c2 lh height left top width bottomLeftForm bottomRightForm gradientForm topLeftForm topRightForm |	aCanvas		fillRectangle: bounds		fillStyle: 			"(color adjustSaturation: -0.3 brightness: 0)"			(Color h: color hue s: color saturation * 0.3 v: color brightness "*0+ 0.8").	rect _ bounds insetBy: 2@3.	c _ self fillStyle asColor.	self isPressed		ifFalse: [			self mouseIsOver				ifTrue: [ c _ c adjustSaturation: 0.0 brightness: 0.0 ]				ifFalse: [					"c _ c adjustSaturation: -0.02 brightness: 0.03"					c _ Color h: c hue s: c saturation * 0.5 v: c brightness 					].			c2 _ c * ColorTheme current buttonGradientBottomFactor.			gradientForm _ self class buttonGradient.			topLeftForm _ self class roundedCornerTL.			topRightForm _ self class roundedCornerTR.			bottomLeftForm _ self class roundedCornerBL.			bottomRightForm _ self class roundedCornerBR ]		ifTrue: ["			c _ c adjustSaturation: -0.15 brightness: 0.0."			c _ c adjustSaturation: 0.1 brightness: -0.1.			c2 _ c * ColorTheme current buttonGradientTopFactor.			gradientForm _ self class buttonGradientPressed.			topLeftForm _ self class roundedCornerTLPressed.			topRightForm _ self class roundedCornerTRPressed.			bottomLeftForm _ self class roundedCornerBLPressed.			bottomRightForm _ self class roundedCornerBRPressed ].			lh _ gradientForm form height.	r _ topLeftForm width.	left _ rect left + r.	top _ rect top.	width _ rect width - r-r.	height _ rect height -r min: lh.	aCanvas fillRectangle: (left@top extent: width@height)  infiniteForm: gradientForm multipliedBy: c.	aCanvas image: topLeftForm multipliedBy: c in: (rect topLeft extent: r@height).	aCanvas image: topRightForm multipliedBy: c in: (rect topRight - (r@0)extent: r@height).	aCanvas fillRectangle: (rect origin + (0@height) extent: rect width@(rect height-height-r)) fillStyle: c2.	aCanvas image: bottomLeftForm multipliedBy: c at: rect bottomLeft - (0@r).	aCanvas image: bottomRightForm multipliedBy: c at: rect bottomRight - (r@r) .	aCanvas fillRectangle: (rect bottomLeft+(r@ r negated) corner:  rect bottomRight - (r@0) ) fillStyle: c2.				f _ self fontToUse.	center _ bounds center.	label ifNotNil: [		w _ f widthOfString: label.		x _ bounds width-14 > w			ifTrue: [ center x - (w // 2) ]			ifFalse: [ bounds left +7].		y _ center y - (f height // 2).		aCanvas drawStringEmbossed: label in: (x@y extent: bounds extent - (14@4)) font: f color: ColorTheme current buttonLabel"		aCanvas drawString: label in: (x@y extent: bounds extent - (20@4)) font: f color: ColorTheme current buttonLabel"		"aCanvas drawString: label at: x@y font: f color: ColorTheme current buttonLabel" 		]! !!PluggableButtonMorph methodsFor: 'event handling' stamp: 'jmv 11/30/2010 12:00'!mouseUp: evt	isPressed _ false.	(self containsPoint: evt cursorPoint)		ifTrue: [ self performAction ]		ifFalse: [ mouseIsOver _ false ].	self changed.! !!PluggableButtonMorph class methodsFor: 'class initialization' stamp: 'jmv 11/30/2010 11:25'!initialize	"	PluggableButtonMorph initialize	"	ButtonGradient _ ButtonGradientPressed _ RoundedCornerBL _ RoundedCornerBLPressed _ RoundedCornerBR _ RoundedCornerBRPressed _ RoundedCornerTL _ RoundedCornerTLPressed _ RoundedCornerTR _ RoundedCornerTRPressed _ nil.! !!PluggableButtonMorph class methodsFor: 'accessing - forms' stamp: 'jmv 11/30/2010 11:12'!buttonGradient	"	ButtonGradient _ nil.	"	| h gradientBottomFactor gradientTopFactor |	h _ ColorTheme current buttonGradientHeight.	(ButtonGradient isNil or: [ ButtonGradient form height ~= h ]) ifTrue: [		gradientTopFactor _ ColorTheme current buttonGradientTopFactor.		ColorTheme current useButtonGradient			ifTrue: [				gradientBottomFactor _ ColorTheme current buttonGradientBottomFactor ]			ifFalse: [				gradientBottomFactor _ ColorTheme current buttonGradientTopFactor ].		ButtonGradient _ InfiniteForm 		verticalGradient: h		topColor: Color white * gradientTopFactor		bottomColor: Color white * gradientBottomFactor ].	^ ButtonGradient! !!PluggableButtonMorph class methodsFor: 'accessing - forms' stamp: 'jmv 11/30/2010 11:12'!buttonGradientPressed	"	ButtonGradientPressed _ nil.	"	| h gradientBottomFactor gradientTopFactor |	h _ ColorTheme current buttonGradientHeight.	(ButtonGradientPressed isNil or: [ ButtonGradientPressed form height ~= h ]) ifTrue: [		gradientTopFactor _ ColorTheme current buttonGradientBottomFactor.		ColorTheme current useButtonGradient			ifTrue: [				gradientBottomFactor _ ColorTheme current buttonGradientTopFactor ]			ifFalse: [				gradientBottomFactor _ ColorTheme current buttonGradientBottomFactor ].		ButtonGradientPressed _ InfiniteForm 		verticalGradient: h		topColor: Color white * gradientTopFactor		bottomColor: Color white * gradientBottomFactor ].	^ ButtonGradientPressed! !!PluggableButtonMorph class methodsFor: 'accessing - forms' stamp: 'jmv 11/30/2010 11:00'!roundedCornerBL	"	RoundedCornerBL _ nil.	Display getCanvas translucentImage: self roundedCornerBL at: 520@60	"	| f r gradientBottomFactor |	r _ ColorTheme current roundedButtonRadius.	(RoundedCornerBL notNil and: [		RoundedCornerBL width = r and: [ RoundedCornerBL height = r ]]) ifFalse: [		ColorTheme current useButtonGradient			ifTrue: [				gradientBottomFactor _ ColorTheme current buttonGradientBottomFactor ]			ifFalse: [				gradientBottomFactor _ ColorTheme current buttonGradientTopFactor ].		f _ Form			bottomLeftCorner: r			height: r			gradientTop: gradientBottomFactor			gradientBottom: gradientBottomFactor.		RoundedCornerBL _ f ].	^ RoundedCornerBL! !!PluggableButtonMorph class methodsFor: 'accessing - forms' stamp: 'jmv 11/30/2010 11:07'!roundedCornerBLPressed	"	RoundedCornerBLPressed _ nil.	Display getCanvas translucentImage: self roundedCornerBLPressed at: 620@60	"	| f r gradientBottomFactor |	r _ ColorTheme current roundedButtonRadius.	(RoundedCornerBLPressed notNil and: [		RoundedCornerBLPressed width = r and: [ RoundedCornerBLPressed height = r ]]) ifFalse: [		ColorTheme current useButtonGradient			ifTrue: [				gradientBottomFactor _ ColorTheme current buttonGradientTopFactor ]			ifFalse: [				gradientBottomFactor _ ColorTheme current buttonGradientBottomFactor ].		f _ Form			bottomLeftCorner: r			height: r			gradientTop: gradientBottomFactor			gradientBottom: gradientBottomFactor.		RoundedCornerBLPressed _ f ].	^ RoundedCornerBLPressed! !!PluggableButtonMorph class methodsFor: 'accessing - forms' stamp: 'jmv 11/30/2010 11:05'!roundedCornerBR	"	RoundedCornerBR _ nil.	Display getCanvas translucentImage: self roundedCornerBR at: 540@60	"	| f r gradientBottomFactor |	r _ ColorTheme current roundedButtonRadius.	(RoundedCornerBR notNil and: [		RoundedCornerBR width = r and: [ RoundedCornerBR height = r ]]) ifFalse: [		ColorTheme current useButtonGradient			ifTrue: [				gradientBottomFactor _ ColorTheme current buttonGradientBottomFactor ]			ifFalse: [				gradientBottomFactor _ ColorTheme current buttonGradientTopFactor ].		f _ Form			bottomRightCorner: r			height: r			gradientTop: gradientBottomFactor			gradientBottom: gradientBottomFactor.		RoundedCornerBR _ f ].	^ RoundedCornerBR! !!PluggableButtonMorph class methodsFor: 'accessing - forms' stamp: 'jmv 11/30/2010 11:07'!roundedCornerBRPressed	"	RoundedCornerBRPressed _ nil.	Display getCanvas translucentImage: self roundedCornerBRPressed at: 640@60	"	| f r gradientBottomFactor |	r _ ColorTheme current roundedButtonRadius.	(RoundedCornerBRPressed notNil and: [		RoundedCornerBRPressed width = r and: [ RoundedCornerBRPressed height = r ]]) ifFalse: [		ColorTheme current useButtonGradient			ifTrue: [				gradientBottomFactor _ ColorTheme current buttonGradientTopFactor ]			ifFalse: [				gradientBottomFactor _ ColorTheme current buttonGradientBottomFactor ].		f _ Form			bottomRightCorner: r			height: r			gradientTop: gradientBottomFactor			gradientBottom: gradientBottomFactor.		RoundedCornerBRPressed _ f ].	^ RoundedCornerBRPressed! !!PluggableButtonMorph class methodsFor: 'accessing - forms' stamp: 'jmv 11/30/2010 11:00'!roundedCornerTL	"	RoundedCornerTL _ nil.	Display getCanvas translucentImage: self roundedCornerTL at: 520@10	"	| height f r gradientBottomFactor gradientTopFactor |	height _ ColorTheme current buttonGradientHeight.	r _ ColorTheme current roundedButtonRadius.	(RoundedCornerTL notNil and: [		RoundedCornerTL width = r and: [ RoundedCornerTL height = height ]]) ifFalse: [		gradientTopFactor _ ColorTheme current buttonGradientTopFactor.		ColorTheme current useButtonGradient			ifTrue: [				gradientBottomFactor _ ColorTheme current buttonGradientBottomFactor ]			ifFalse: [				gradientBottomFactor _ ColorTheme current buttonGradientTopFactor ].		f _ Form			topLeftCorner: r			height: height			gradientTop: gradientTopFactor			gradientBottom: gradientBottomFactor.		RoundedCornerTL _ f ].	^ RoundedCornerTL! !!PluggableButtonMorph class methodsFor: 'accessing - forms' stamp: 'jmv 11/30/2010 11:08'!roundedCornerTLPressed	"	RoundedCornerTLPressed _ nil.	Display getCanvas translucentImage: self roundedCornerTLPressed at: 620@10	"	| height f r gradientBottomFactor gradientTopFactor |	height _ ColorTheme current buttonGradientHeight.	r _ ColorTheme current roundedButtonRadius.	(RoundedCornerTLPressed notNil and: [		RoundedCornerTLPressed width = r and: [ RoundedCornerTLPressed height = height ]]) ifFalse: [		gradientTopFactor _ ColorTheme current buttonGradientBottomFactor.		ColorTheme current useButtonGradient			ifTrue: [				gradientBottomFactor _ ColorTheme current buttonGradientTopFactor ]			ifFalse: [				gradientBottomFactor _ ColorTheme current buttonGradientBottomFactor ].		f _ Form			topLeftCorner: r			height: height			gradientTop: gradientTopFactor			gradientBottom: gradientBottomFactor.		RoundedCornerTLPressed _ f ].	^ RoundedCornerTLPressed! !!PluggableButtonMorph class methodsFor: 'accessing - forms' stamp: 'jmv 11/30/2010 11:00'!roundedCornerTR	"	RoundedCornerTR _ nil.	Display getCanvas translucentImage: self roundedCornerTR at: 540@10	"	| height f r gradientBottomFactor gradientTopFactor |	height _ ColorTheme current buttonGradientHeight.	r _ ColorTheme current roundedButtonRadius.	(RoundedCornerTR notNil and: [		RoundedCornerTR width = r and: [ RoundedCornerTR height = height ]]) ifFalse: [		gradientTopFactor _ ColorTheme current buttonGradientTopFactor.		ColorTheme current useButtonGradient			ifTrue: [				gradientBottomFactor _ ColorTheme current buttonGradientBottomFactor ]			ifFalse: [				gradientBottomFactor _ ColorTheme current buttonGradientTopFactor ].		f _ Form			topRightCorner: r			height: height			gradientTop: gradientTopFactor			gradientBottom: gradientBottomFactor.		RoundedCornerTR _ f ].	^ RoundedCornerTR! !!PluggableButtonMorph class methodsFor: 'accessing - forms' stamp: 'jmv 11/30/2010 11:08'!roundedCornerTRPressed	"	RoundedCornerTR _ nil.	Display getCanvas translucentImage: self roundedCornerTRPressed at: 640@10	"	| height f r gradientBottomFactor gradientTopFactor |	height _ ColorTheme current buttonGradientHeight.	r _ ColorTheme current roundedButtonRadius.	(RoundedCornerTRPressed notNil and: [		RoundedCornerTRPressed width = r and: [ RoundedCornerTRPressed height = height ]]) ifFalse: [		gradientTopFactor _ ColorTheme current buttonGradientBottomFactor.		ColorTheme current useButtonGradient			ifTrue: [				gradientBottomFactor _ ColorTheme current buttonGradientTopFactor ]			ifFalse: [				gradientBottomFactor _ ColorTheme current buttonGradientBottomFactor ].		f _ Form			topRightCorner: r			height: height			gradientTop: gradientTopFactor			gradientBottom: gradientBottomFactor.		RoundedCornerTRPressed _ f ].	^ RoundedCornerTRPressed! !!SoftColorTheme methodsFor: 'colors' stamp: 'jmv 11/30/2010 12:02'!buttonLabel	^Color gray: 0.25! !!SoftColorTheme methodsFor: 'other options' stamp: 'jmv 11/30/2010 10:50'!roundButtons	^true! !!SoftColorTheme methodsFor: 'other options' stamp: 'jmv 11/30/2010 10:59'!useButtonGradient	^true! !!SoftColorTheme methodsFor: 'menu colors' stamp: 'jmv 11/30/2010 09:18'!menu	Display depth <= 2 ifTrue: [^ Color white].	^Color r: 0.85 g: 0.85 b: 0.85 alpha: 0.85! !!SystemWindow methodsFor: 'drawing' stamp: 'jmv 11/30/2010 10:47'!drawWindowBodyOn: aCanvas roundCorners: doRoundCorners widgetsColor: widgetsColor	"Title area is not inside window borders"	| r bl tl tr he tw bw |	doRoundCorners		ifFalse: [			borderColor class == Symbol				ifTrue: [					" This would of course be much better...					^aCanvas fillRectangle: bounds fillStyle: self fillStyle borderWidth: borderWidth borderStyleSymbol: borderColor					"					aCanvas fillRectangle: bounds fillStyle: self fillStyle borderWidth: borderWidth borderStyleSymbol: borderColor baseColorForBorder: self raisedColor ]				ifFalse: [					aCanvas fillRectangle: bounds fillStyle: self fillStyle borderWidth: borderWidth borderStyleSymbol: #simple baseColorForBorder: borderColor ]]		ifTrue: [			r _ ColorTheme current roundedWindowRadius.			aCanvas image: SystemWindow roundedCornerBL multipliedBy: widgetsColor at: bounds bottomLeft - (0@r).			aCanvas image: SystemWindow roundedCornerBR multipliedBy: widgetsColor at: bounds bottomRight - (r@r) .			aCanvas fillRectangle: self innerBounds fillStyle: self fillStyle.			tl _ bounds topLeft + (0@self labelHeight).			tr _ bounds topRight + (borderWidth negated@self labelHeight).			bl _ bounds bottomLeft + (r@borderWidth negated).			he _ borderWidth@(bounds height - self labelHeight - r).			tw _ bounds width@borderWidth.			bw _ bounds width - r - r@borderWidth.			aCanvas fillRectangle: (tl extent: he) fillStyle: widgetsColor.			aCanvas fillRectangle: (tr extent: he) fillStyle: widgetsColor.			aCanvas fillRectangle: (bl extent: bw) fillStyle: widgetsColor.			aCanvas fillRectangle: (tl extent: tw) fillStyle: widgetsColor ]! !!SystemWindow methodsFor: 'drawing' stamp: 'jmv 11/30/2010 10:47'!drawWindowTitleAreaOn: aCanvas roundCorners: doRoundCorners titleColor: titleColor useTitleGradient: useTitleGradient	| r h c |	h _ self labelHeight.	c _ useTitleGradient ifTrue: [ titleColor * ColorTheme current titleGradientExtraLightness ] ifFalse: [ titleColor ].	doRoundCorners		ifFalse: [			useTitleGradient				ifTrue: [ aCanvas fillRectangle: self titleAreaRect infiniteForm: (SystemWindow titleGradient: h) multipliedBy: c ]				ifFalse: [ aCanvas fillRectangle: self titleAreaRect color: c ]]		ifTrue: [			r _ ColorTheme current roundedWindowRadius.			useTitleGradient				ifTrue: [					aCanvas fillRectangle: (self titleAreaRect insetBy: r@0) infiniteForm: (SystemWindow titleGradient: h) multipliedBy: c.					aCanvas image: (SystemWindow roundedCornerTL: h) multipliedBy: c at: bounds topLeft.					aCanvas image: (SystemWindow roundedCornerTR: h) multipliedBy: c at: bounds topRight - (r@0)				]				ifFalse: [					aCanvas fillRectangle: (self titleAreaRect insetBy: r@0) color: c.					aCanvas image: (SystemWindow roundedCornerTL: h) multipliedBy: c at: bounds topLeft.					aCanvas image: (SystemWindow roundedCornerTR: h) multipliedBy: c at: bounds topRight - (r@0) 				]		]! !!SystemWindow class methodsFor: 'accessing - forms' stamp: 'jmv 11/30/2010 10:47'!roundedCornerBL	"	RoundedCornerBL _ nil.	Display getCanvas translucentImage: self roundedCornerBL at: 10@60	"	| f r |	r _ ColorTheme current roundedWindowRadius.	(RoundedCornerBL notNil and: [		RoundedCornerBL width = r and: [ RoundedCornerBL height = r ]]) ifFalse: [		f _ Form			bottomLeftCorner: r			height: r			gradientTop: 1.0			gradientBottom: 1.0.		RoundedCornerBL _ f ].	^ RoundedCornerBL! !!SystemWindow class methodsFor: 'accessing - forms' stamp: 'jmv 11/30/2010 10:47'!roundedCornerBR	"	RoundedCornerBR _ nil.	Display getCanvas translucentImage: self roundedCornerBR at: 10@60	"	| f r |	r _ ColorTheme current roundedWindowRadius.	(RoundedCornerBR notNil and: [		RoundedCornerBR width = r and: [ RoundedCornerBR height = r ]]) ifFalse: [		f _ Form			bottomRightCorner: r			height: r			gradientTop: 1.0			gradientBottom: 1.0.		RoundedCornerBR _ f ].	^ RoundedCornerBR! !!SystemWindow class methodsFor: 'accessing - forms' stamp: 'jmv 11/30/2010 10:47'!roundedCornerTL: height	"	RoundedCornerTL _ nil.	Display getCanvas translucentImage: (self roundedCornerTL:20) at: 520@10	"	| f r gradientBottomFactor gradientTopFactor |	r _ ColorTheme current roundedWindowRadius.	(RoundedCornerTL notNil and: [		RoundedCornerTL width = r and: [ RoundedCornerTL height = height ]]) ifFalse: [		ColorTheme current useWindowTitleGradient			ifTrue: [				gradientTopFactor _ ColorTheme current titleGradientTopFactor.				gradientBottomFactor _ ColorTheme current titleGradientBottomFactor ]			ifFalse: [				gradientTopFactor _ 1.0.				gradientBottomFactor _ 1.0 ].		f _ Form			topLeftCorner: r			height: height			gradientTop: gradientTopFactor			gradientBottom: gradientBottomFactor.		RoundedCornerTL _ f ].	^ RoundedCornerTL! !!SystemWindow class methodsFor: 'accessing - forms' stamp: 'jmv 11/30/2010 10:47'!roundedCornerTR: height	"	RoundedCornerTR _ nil.	Display getCanvas translucentImage: self roundedCornerTR at: 10@60	"	| f r gradientBottomFactor gradientTopFactor |	r _ ColorTheme current roundedWindowRadius.	(RoundedCornerTR notNil and: [		RoundedCornerTR width = r and: [ RoundedCornerTR height = height ]]) ifFalse: [		ColorTheme current useWindowTitleGradient			ifTrue: [				gradientTopFactor _ ColorTheme current titleGradientTopFactor.				gradientBottomFactor _ ColorTheme current titleGradientBottomFactor ]			ifFalse: [				gradientTopFactor _ 1.0.				gradientBottomFactor _ 1.0 ].		f _ Form			topRightCorner: r			height: height			gradientTop: gradientTopFactor			gradientBottom: gradientBottomFactor.		RoundedCornerTR _ f ].	^ RoundedCornerTR! !!SystemWindow class methodsFor: 'accessing - forms' stamp: 'jmv 11/30/2010 10:43'!titleGradient: h	"	TitleGradient _ nil.	"	(TitleGradient isNil or: [ TitleGradient form height ~= h ]) ifTrue: [		TitleGradient _ InfiniteForm 		verticalGradient: h		topColor: Color white * ColorTheme current titleGradientTopFactor		bottomColor: Color white * ColorTheme current titleGradientBottomFactor ].	^ TitleGradient! !!TextMorphForEditView methodsFor: 'event handling' stamp: 'jmv 11/30/2010 09:15'!keyboardFocusChange: aBoolean	"The message is sent to a morph when its keyboard focus changes.	The given argument indicates that the receiver is gaining (versus losing) the keyboard focus.	In this case, all we need to do is to redraw border feedback"	super keyboardFocusChange: aBoolean.	editView invalidateBorderFeedback! !!SystemWindow class reorganize!('accessing' classVersion)('instance creation' includeInNewMorphMenu labelled:)('top window' closeTopWindow noteTopWindowIn: sendTopWindowToBack wakeUpTopWindowUponStartup windowsIn:satisfying:)('class initialization' initialize)('accessing - forms' roundedCornerBL roundedCornerBR roundedCornerTL: roundedCornerTR: titleGradient:)('accessing - icons' closeIcon collapseIcon expandIcon menuIcon)!PluggableButtonMorph initialize!!classDefinition: #PluggableButtonMorph category: #'Morphic-Windows'!PluggableMorph subclass: #PluggableButtonMorph	instanceVariableNames: 'label font getStateSelector actionSelector getLabelSelector getMenuSelector arguments argumentsProvider argumentsSelector isPressed mouseIsOver'	classVariableNames: 'ButtonGradient ButtonGradientPressed RoundedCornerBL RoundedCornerBLPressed RoundedCornerBR RoundedCornerBRPressed RoundedCornerTL RoundedCornerTLPressed RoundedCornerTR RoundedCornerTRPressed'	poolDictionaries: ''	category: 'Morphic-Windows'!FormCanvas removeSelector: #fillRectangle:tilingWith:rule:!ColorTheme removeSelector: #roundedCornerRadius!