'From Cuis 2.0 of 4 January 2010 [latest update: #393] on 8 January 2010 at 3:32:36 pm'!!PolygonMorph methodsFor: 'geometry testing' stamp: 'jmv 1/8/2010 15:28'!containsPoint: aPoint	(bounds containsPoint: aPoint) ifFalse: [^ false].  "quick elimination"	closed & color isTransparent not ifTrue:		[^ (self filledForm pixelValueAt: aPoint - bounds topLeft + 1) > 0].	self lineSegmentsDo:		[:p1 :p2 |		(aPoint onLineFrom: p1 to: p2 within: (3 max: borderWidth+1//2) asFloat)				ifTrue: [^ true]].	self arrowForms do:		[:f | (f pixelValueAt: aPoint - f offset) > 0 ifTrue: [^ true]].	^ false! !!ScrollPane methodsFor: 'geometry testing' stamp: 'jmv 1/8/2010 15:28'!containsPoint: aPoint	(bounds containsPoint: aPoint) ifTrue: [^ true].		"Also include v scrollbar when it is extended..."	((retractableScrollBar and: [ self vIsScrollbarShowing ]) and:		[scrollBar containsPoint: aPoint])			ifTrue:[ ^true ].			"Also include hScrollbar when it is extended..."	^(retractableScrollBar and: [self hIsScrollbarShowing]) and:		[hScrollBar containsPoint: aPoint]! !!SketchMorph methodsFor: 'geometry testing' stamp: 'jmv 1/8/2010 15:29'!containsPoint: aPoint	^ (self bounds containsPoint: aPoint) and: [		(originalForm isTransparentAt: aPoint - bounds origin) not ]! !!ColorPickerMorph methodsFor: 'geometry testing' stamp: 'jmv 1/8/2010 15:29'!containsPoint: aPoint	^ (bounds containsPoint: aPoint)		or: [ RevertBox containsPoint: aPoint - self topLeft ]! !!TransformMorph methodsFor: 'geometry testing' stamp: 'jmv 1/8/2010 15:30'!containsPoint: aPoint	(bounds containsPoint: aPoint) ifFalse: [^ false].  "quick elimination"	self hasSubmorphs		ifTrue: [			self submorphsDo: [ :m |				(m containsPoint: (transform globalPointToLocal: aPoint))					ifTrue: [^ true]].			^ false]		ifFalse: [^ true]! !