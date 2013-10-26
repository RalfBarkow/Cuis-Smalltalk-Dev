'From Cuis 3.0 of 18 January 2011 [latest update: #768] on 4 March 2011 at 3:00:13 pm'!!classDefinition: #FormCanvas category: #'Morphic-Support'!Canvas subclass: #FormCanvas	instanceVariableNames: 'origin clipRect form port shadowColor auxForm auxBlitter '	classVariableNames: 'AuxForm AccessProtect AuxBlitter '	poolDictionaries: ''	category: 'Morphic-Support'!!FormCanvas methodsFor: 'drawing-images' stamp: 'jmv 3/4/2011 14:41'!image: aForm multipliedBy: aColor at: aPoint	"Multiply aForm and aColor, then blend over destination.	aForm is a kind of advanced stencil, supplying brightness and opacity at each pixel	Display getCanvas image: (SystemWindow roundedCornerTR: 20)multipliedBy: Color red at: 20@20	"	AccessProtect critical: [		self buildAuxWith: aForm multipliedWith: aColor.		self translucentImage: AuxForm at: aPoint sourceRect: aForm boundingBox ]! !!FormCanvas methodsFor: 'drawing-images' stamp: 'jmv 3/4/2011 14:41'!image: aForm multipliedBy: aColor in: aRectangle	"Multiply aForm and aColor, then blend over destination.	aForm is a kind of advanced stencil, supplying brightness and opacity at each pixel	Display getCanvas image: (SystemWindow roundedCornerTR: 20)multipliedBy: Color red at: 20@20	"	| h w |	AccessProtect critical: [		self buildAuxWith: aForm multipliedWith: aColor.		w _ aForm width min: aRectangle width.		h _ aForm height min: aRectangle height.		self translucentImage: AuxForm at: aRectangle topLeft sourceRect: (0@0 extent: w@h) ]! !!FormCanvas methodsFor: 'drawing-rectangles' stamp: 'jmv 3/4/2011 14:41'!fillRectangle: aRectangle infiniteForm: anInfiniteForm multipliedBy: aColor	"Fill aRectangle with the equivalent of anInfiniteForm multiplied by aColor	aForm is a kind of advanced stencil, supplying brightness and opacity at each pixel	Similar to #image:multipliedBy:at:	Display getCanvas fillRectangle: (10@10 extent: 100@100) infiniteForm: (SystemWindow titleGradient: 12) multipliedBy: Color red.	"	| f |	f _ anInfiniteForm form.	AccessProtect critical: [		self buildAuxWith: f multipliedWith: aColor.		self fillRectangle: aRectangle tilingWith: AuxForm sourceRect: f boundingBox rule: Form paint ]! !!FormCanvas methodsFor: 'private' stamp: 'jmv 3/4/2011 10:42'!buildAuxWith: aForm multipliedWith: aColor	| h w r |	w _ aForm width.	h _ aForm height.	AuxForm		ifNotNil: [			w _ w max: AuxForm width.			h _ h max: AuxForm height.			(AuxForm width < w or: [ AuxForm height < h ]) ifTrue: [ AuxForm _ nil ]].	AuxForm		ifNil: [			AuxForm _ Form extent: w@h depth: 32.			AuxBlitter _ BitBlt toForm: AuxForm ].		r _ aForm boundingBox.	AuxForm fill: r fillColor: aColor.	AuxBlitter		sourceForm: aForm;		combinationRule: Form rgbMul;		sourceRect: r;		copyBits.! !!FormCanvas class methodsFor: 'class initialization' stamp: 'jmv 3/4/2011 14:39'!initialize	"FormCanvas initialize"	Smalltalk addToShutDownList: self.	AccessProtect _ Semaphore forMutualExclusion.! !!FormCanvas class methodsFor: 'system startup' stamp: 'jmv 3/4/2011 10:35'!shutDown	AuxForm _ nil.	AuxBlitter _ nil! !!PasteUpMorph methodsFor: 'caching' stamp: 'jmv 3/4/2011 14:46'!releaseCachedState	super releaseCachedState.	self isWorldMorph ifTrue:[self cleanseStepList].	backgroundImage _ nil.	worldState canvas: nil.! !!PasteUpMorph class methodsFor: 'system startup' stamp: 'jmv 3/4/2011 14:47'!shutDown		World ifNotNil:[		World triggerEvent: #aboutToLeaveWorld.		World releaseCachedState ]! !PasteUpMorph removeSelector: #nilMagnifiedBackgroundImage!FormCanvas initialize!!classDefinition: #FormCanvas category: #'Morphic-Support'!Canvas subclass: #FormCanvas	instanceVariableNames: 'origin clipRect form port shadowColor'	classVariableNames: 'AccessProtect AuxBlitter AuxForm'	poolDictionaries: ''	category: 'Morphic-Support'!