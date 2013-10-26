'From Cuis 2.6 of 10 August 2010 [latest update: #540] on 29 August 2010 at 6:48:33 pm'!!ProtoObject methodsFor: 'testing' stamp: 'jmv 8/29/2010 18:46'!ifNil: nilBlock ifNotNil: ifNotNilBlock	"Evaluate the block, unless I'm == nil (q.v.)"	^ ifNotNilBlock valueWithPossibleArgument: self! !!ProtoObject methodsFor: 'testing' stamp: 'jmv 8/29/2010 18:47'!ifNotNil: ifNotNilBlock	"Evaluate the block, unless I'm == nil (q.v.)"	^ ifNotNilBlock valueWithPossibleArgument: self! !!ProtoObject methodsFor: 'testing' stamp: 'jmv 8/29/2010 18:47'!ifNotNil: ifNotNilBlock ifNil: nilBlock 	"If I got here, I am not nil, so evaluate the block ifNotNilBlock"	^ ifNotNilBlock valueWithPossibleArgument: self! !!BlockClosure methodsFor: 'evaluating' stamp: 'jmv 8/29/2010 18:44'!ifError: errorHandlerBlock	"Evaluate the block represented by the receiver, and normally return it's value.  If an error occurs, the errorHandlerBlock is evaluated, and it's value is instead returned.  The errorHandlerBlock must accept zero, one, or two parameters (the error message and the receiver)."	"Examples:		[1 whatsUpDoc] ifError: [:err :rcvr | 'huh?'].		[1 / 0] ifError: [:err :rcvr |			'ZeroDivide' = err				ifTrue: [Float infinity]				ifFalse: [self error: err]]"	^ self on: Error do: [ :ex |		errorHandlerBlock valueWithPossibleArgument: ex description and: ex receiver ]! !!BlockClosure methodsFor: 'evaluating' stamp: 'jmv 8/29/2010 18:45'!valueWithPossibleArgs: anArray	"Generally, prefer #valueWithPossibleArgument: and #valueWithPossibleArgument:and:	for performance."	^ numArgs = 0		ifTrue: [ self value ]		ifFalse: [			self valueWithArguments:				(numArgs = anArray size					ifTrue: [ anArray ]					ifFalse: [						numArgs > anArray size							ifTrue: [ anArray , (Array new: numArgs - anArray size) ]							ifFalse: [								anArray									copyFrom: 1									to: numArgs ]]) ].! !!BlockClosure methodsFor: 'evaluating' stamp: 'jmv 8/29/2010 18:33'!valueWithPossibleArgument: anArg 	"Evaluate the block represented by the receiver. 	 If the block requires one argument, use anArg"	numArgs = 0 ifTrue: [ ^self value ].	^self value: anArg! !!BlockClosure methodsFor: 'evaluating' stamp: 'jmv 8/29/2010 18:48'!valueWithPossibleArgument: anArg and: secondArg	"Evaluate the block represented by the receiver. 	 If the block requires one argument, use anArg, 	if it requires two, use anArg and secondArg.	Squeak uses #cull:, #cull:cull:, etc. I (jmv) find that name quite obscure"	numArgs = 0 ifTrue: [ ^self value ].	numArgs = 1 ifTrue: [ ^self value: anArg ].	^self value: anArg value: secondArg! !!BlockClosure methodsFor: 'exceptions' stamp: 'jmv 8/29/2010 18:44'!onDNU: selector do: handleBlock	"Catch MessageNotUnderstood exceptions but only those of the given selector (DNU stands for doesNotUnderstand:)"	^ self on: MessageNotUnderstood do: [ :exception |		exception message selector = selector			ifTrue: [ handleBlock valueWithPossibleArgument: exception ]			ifFalse: [ exception pass ]	  ]! !!ContextPart methodsFor: 'private-exceptions' stamp: 'jmv 8/29/2010 18:30'!handleSignal: exception	"Sent to handler (on:do:) contexts only.  If my exception class (first arg) handles exception then execute my handle block (second arg), otherwise forward this message to the next handler context.  If none left, execute exception's defaultAction (see nil>>handleSignal:)."	| val |	(((self tempAt: 1) handles: exception) and: [ self tempAt: 3 ]) ifFalse: [		^ self nextHandlerContext handleSignal: exception ].	exception privHandlerContext: self contextTag.	self tempAt: 3 put: false.  "disable self while executing handle block"	val _ [ (self tempAt: 2) valueWithPossibleArgument: exception ]		ensure: [ self tempAt: 3 put: true ].	self return: val.  "return from self if not otherwise directed in handle block"! !!BlockContext methodsFor: 'evaluating' stamp: 'jmv 8/29/2010 18:44'!ifError: errorHandlerBlock	"Evaluate the block represented by the receiver, and normally return it's value.  If an error occurs, the errorHandlerBlock is evaluated, and it's value is instead returned.  The errorHandlerBlock must accept zero, one, or two parameters (the error message and the receiver)."	"Examples:		[1 whatsUpDoc] ifError: [:err :rcvr | 'huh?'].		[1 / 0] ifError: [:err :rcvr |			'ZeroDivide' = err				ifTrue: [Float infinity]				ifFalse: [self error: err]]"	^ self on: Error do: [ :ex |		errorHandlerBlock valueWithPossibleArgument: ex description and: ex receiver ]! !!BlockContext methodsFor: 'evaluating' stamp: 'jmv 8/29/2010 18:34'!valueWithPossibleArgument: anArg 	"Evaluate the block represented by the receiver. 	 If the block requires one argument, use anArg"	nargs = 0 ifTrue: [ ^self value ].	^self value: anArg! !!BlockContext methodsFor: 'evaluating' stamp: 'jmv 8/29/2010 18:42'!valueWithPossibleArgument: anArg and: secondArg	"Evaluate the block represented by the receiver. 	 If the block requires one argument, use anArg, 	if it requires two, use anArg and secondArg"	nargs = 0 ifTrue: [ ^self value ].	nargs = 1 ifTrue: [ ^self value: anArg ].	^self value: anArg value: secondArg! !!BlockContext methodsFor: 'exceptions' stamp: 'jmv 8/29/2010 18:45'!onDNU: selector do: handleBlock	"Catch MessageNotUnderstood exceptions but only those of the given selector (DNU stands for doesNotUnderstand:)"	^ self on: MessageNotUnderstood do: [ :exception |		exception message selector = selector			ifTrue: [ handleBlock valueWithPossibleArgument: exception ]			ifFalse: [ exception pass ]	  ]! !!UndefinedObject methodsFor: 'evaluating' stamp: 'jmv 8/29/2010 18:46'!valueWithPossibleArgument: anArg	"To provide polymorphism with blocks. Allows nil to be used instead of an empty closure."! !!UndefinedObject methodsFor: 'evaluating' stamp: 'jmv 8/29/2010 18:46'!valueWithPossibleArgument: anArg and: secondArg	"To provide polymorphism with blocks. Allows nil to be used instead of an empty closure."! !