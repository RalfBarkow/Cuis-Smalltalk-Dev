'From Cuis 3.1 of 4 March 2011 [latest update: #850] on 6 April 2011 at 10:16:18 am'!!Color methodsFor: 'printing' stamp: 'jmv 4/5/2011 10:20'!shortPrintOn: aStream	"Return a short (but less precise) print string for use where space is tight."	 self name ifNotNil: [ :name |		^ aStream			nextPutAll: name ].	aStream		nextPutAll: '(r:';		nextPutAll: (self red roundTo: 0.01) printString;		nextPutAll: ' g:';		nextPutAll: (self green roundTo: 0.01) printString;		nextPutAll: ' b:';		nextPutAll: (self blue roundTo: 0.01) printString;		nextPutAll: ')'! !!Color methodsFor: 'printing' stamp: 'jmv 4/5/2011 09:55'!shortPrintString	"Return a short (but less precise) print string for use where space is tight."	^String streamContents: [ :strm |		self shortPrintOn: strm ]! !