'From Cuis 3.2 of 12 April 2011 [latest update: #914] on 14 April 2011 at 7:50:22 pm'!!classDefinition: #ScrollBar category: #'Morphic-Views for Models'!PluggableMorph subclass: #ScrollBar	instanceVariableNames: 'slider value setValueSelector sliderShadow sliderColor menuButton upButton downButton pagingArea scrollDelta pageDelta interval menuSelector timeOfMouseDown timeOfLastScroll nextPageDirection currentScrollDelay scrollBarAction '	classVariableNames: ''	poolDictionaries: ''	category: 'Morphic-Views for Models'!!LayoutAdjustingMorph methodsFor: 'event handling' stamp: 'jmv 4/14/2011 19:27'!mouseDown: anEvent	super mouseDown: anEvent.	hand _ anEvent hand.	self startStepping.	Preferences fastDragWindowForMorphic ifTrue: [		indicator _ RectangleIndicatorMorph new.		indicator bounds: self initialIndicatorBounds.		indicator openInWorld ]! !!ScrollBar methodsFor: 'access' stamp: 'jmv 4/14/2011 19:19'!sliderColor: aColor 	"Change the color of the scrollbar to go with aColor."	| buttonColor |	sliderColor _ aColor.	slider ifNotNil: [ slider color: sliderColor ].	buttonColor _ self thumbColor.	upButton color: buttonColor.	downButton color: buttonColor.	slider color: buttonColor slightlyLighter.	pagingArea color: (aColor alphaMixed: 0.3 with: Color white)! !!ScrollBar methodsFor: 'geometry' stamp: 'jmv 4/14/2011 19:21'!totalSliderArea	bounds isWide		ifTrue: [			upButton right > upButton right				ifTrue: [upButton _ upButton].			^upButton bounds topRight corner: downButton bounds bottomLeft]		ifFalse:[			upButton bottom > upButton bottom				ifTrue: [upButton _ upButton].			^upButton bounds bottomLeft corner: downButton bounds topRight].! !!ScrollBar methodsFor: 'initialize' stamp: 'jmv 4/14/2011 19:23'!initializeUpButton	"initialize the receiver's upButton"	upButton _ ScrollbarButton		newBounds: (self innerBounds topLeft extent: self buttonExtent)		color: self thumbColor.	upButton 		on: #mouseDown		send: #scrollUpInit		to: self.	upButton 		on: #mouseUp		send: #finishedScrolling		to: self.	bounds isWide		ifTrue: [ upButton updateLeftButtonImage ]		ifFalse: [ upButton updateUpButtonImage ].	self addMorph: upButton! !!ScrollbarButton methodsFor: 'events-processing' stamp: 'jmv 4/14/2011 19:46'!containsPoint: aPoint	"Little hack to avoid competing with a rounded corner window resizer"	aPoint+1 < bounds bottomRight ifFalse: [^false].	^super containsPoint: aPoint! !!ScrollbarButton reorganize!('initialization' initialize updateDownButtonImage updateLeftButtonImage updateRightButtonImage updateUpButtonImage)('drawing' drawOn:)('geometry' bottom right)('events-processing' containsPoint:)!!classDefinition: #ScrollBar category: #'Morphic-Views for Models'!PluggableMorph subclass: #ScrollBar	instanceVariableNames: 'slider value setValueSelector sliderShadow sliderColor upButton downButton pagingArea scrollDelta pageDelta interval timeOfMouseDown timeOfLastScroll nextPageDirection currentScrollDelay scrollBarAction'	classVariableNames: ''	poolDictionaries: ''	category: 'Morphic-Views for Models'!