'From Squeak3.7 of ''4 September 2004'' [latest update: #5989] on 30 September 2008 at 9:58:46 am'!!Dictionary methodsFor: 'adding' stamp: 'jmv 9/30/2008 09:54'!addAll: aCollection	"It should hold Associations, then"	(aCollection isKindOf: Dictionary) ifFalse: [		^super addAll: aCollection ].		aCollection == self ifFalse: [		aCollection keysAndValuesDo: [:key :value |			self at: key put: value]].		^aCollection! !