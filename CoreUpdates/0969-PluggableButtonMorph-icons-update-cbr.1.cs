'From Cuis 3.2 of 12 April 2011 [latest update: #914] on 9 May 2011 at 1:19:06 am'!!PluggableButtonMorph methodsFor: 'drawing' stamp: 'cbr 5/8/2011 23:30'!drawOn: aCanvas	icon ifNotNil: [ label _ ''. 		Theme current squareWindowLabelButtons			ifTrue: [ self draw3DLookOn: aCanvas. ].		^ self drawInconOn: aCanvas	].	Theme current roundButtons		ifTrue: [ self drawRoundGradientLookOn: aCanvas ]		ifFalse: [ self draw3DLookOn: aCanvas. ].! !