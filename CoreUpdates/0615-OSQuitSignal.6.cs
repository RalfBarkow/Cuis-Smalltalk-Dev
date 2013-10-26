'From Cuis 2.7 of 3 September 2010 [latest update: #600] on 7 October 2010 at 11:43:55 am'!!classDefinition: #EventSensorConstants category: #'Kernel-Processes'!SharedPool subclass: #EventSensorConstants	instanceVariableNames: ''	classVariableNames: 'BlueButtonBit CommandKeyBit CtrlKeyBit EventKeyChar EventKeyDown EventKeyUp EventTypeDragDropFiles EventTypeKeyboard EventTypeMouse EventTypeNone OptionKeyBit RedButtonBit ShiftKeyBit YellowButtonBit EventTypeMenu EventTypeWindow '	poolDictionaries: ''	category: 'Kernel-Processes'!!classDefinition: #PasteUpMorph category: #'Morphic-Worlds'!BorderedMorph subclass: #PasteUpMorph	instanceVariableNames: 'cursor backgroundMorph worldState '	classVariableNames: 'DisableDeferredUpdates WindowEventHandler '	poolDictionaries: ''	category: 'Morphic-Worlds'!!classDefinition: #WindowEvent category: #'Morphic-Events'!MorphicEvent subclass: #WindowEvent	instanceVariableNames: 'action rectangle'	classVariableNames: ''	poolDictionaries: ''	category: 'Morphic-Events'!!WindowEvent commentStamp: '<historical>' prior: 0!I'm an event related to the host window, only dispatched to the World. !!EventSensorConstants class methodsFor: 'pool initialization' stamp: 'bf 8/30/2007 16:33'!initialize	"EventSensorConstants initialize"	RedButtonBit := 4.	BlueButtonBit := 2.	YellowButtonBit := 1.	ShiftKeyBit := 1.	CtrlKeyBit := 2.	OptionKeyBit := 4.	CommandKeyBit := 8.	"Types of events"	EventTypeNone := 0.	EventTypeMouse := 1.	EventTypeKeyboard := 2.	EventTypeDragDropFiles := 3.	EventTypeMenu := 4.	EventTypeWindow := 5.	"Press codes for keyboard events"	EventKeyChar := 0.	EventKeyDown := 1.	EventKeyUp := 2.! !!Morph methodsFor: 'event handling' stamp: 'bf 8/30/2007 20:32'!windowEvent: anEvent	"Host window event"! !!Morph methodsFor: 'events-processing' stamp: 'bf 8/30/2007 20:18'!handleWindowEvent: anEvent	"Handle an event concerning our host window"	anEvent wasHandled ifTrue:[^self]. "not interested"	(self wantsWindowEvent: anEvent) ifFalse:[^self].	anEvent wasHandled: true.	self windowEvent: anEvent.! !!HandMorph methodsFor: 'event handling' stamp: 'jmv 10/7/2010 11:28'!processEvents	"Process user input events from the local input devices."	| evt evtBuf type hadAny |	ActiveEvent ifNotNil: [		"Meaning that we were invoked from within an event response.		Make sure z-order is up to date"		self mouseOverHandler processMouseOver: lastMouseEvent].	hadAny := false.	[ (evtBuf := Sensor nextEvent) isNil ] whileFalse: [		evt := nil.	"for unknown event types"		type := evtBuf first.		type = EventTypeMouse			ifTrue: [ evt := self generateMouseEvent: evtBuf ].		type = EventTypeKeyboard 			ifTrue: [ evt := self generateKeyboardEvent: evtBuf ].		type = EventTypeWindow			ifTrue: [ evt _ self generateWindowEvent: evtBuf ].		"All other events are ignored"		evt			ifNil: [				^hadAny]			ifNotNil: [				"Finally, handle it"				self handleEvent: evt.				hadAny := true.				"For better user feedback, return immediately after a mouse event has been processed."				evt isMouse ifTrue: [ ^hadAny ]]].	"note: if we come here we didn't have any mouse events"	mouseClickState 		ifNotNil: [ 			"No mouse events during this cycle. Make sure click states time out accordingly"			mouseClickState handleEvent: lastMouseEvent asMouseMove from: self].	^hadAny! !!HandMorph methodsFor: 'events-processing' stamp: 'jmv 10/7/2010 11:33'!handleEvent: anEvent	| evt ofs |	owner ifNil:[^self].	evt _ anEvent.	EventStats ifNil:[EventStats _ IdentityDictionary new].	EventStats at: #count put: (EventStats at: #count ifAbsent:[0]) + 1.	EventStats at: evt type put: (EventStats at: evt type ifAbsent:[0]) + 1.	evt isMouseOver ifTrue:[^self sendMouseEvent: evt].ShowEvents == true ifTrue:[	Display fill: (0@0 extent: 250@120) rule: Form over fillColor: Color white.	ofs _ (owner hands indexOf: self) - 1 * 60.	evt printString displayAt: (0@ofs) + (evt isKeyboard ifTrue:[0@30] ifFalse:[0@0]).	self keyboardFocus printString displayAt: (0@ofs)+(0@45).].	"Notify listeners"	self sendListenEvent: evt to: self eventListeners.		evt isWindowEvent ifTrue: [		self sendEvent: evt.		^ self mouseOverHandler processMouseOver: lastMouseEvent ].	evt isKeyboard ifTrue:[		self sendListenEvent: evt to: self keyboardListeners.		self sendKeyboardEvent: evt.		^self mouseOverHandler processMouseOver: lastMouseEvent].	evt isDropEvent ifTrue:[		self sendEvent: evt.		^self mouseOverHandler processMouseOver: lastMouseEvent].	evt isMouse ifTrue:[		self sendListenEvent: evt to: self mouseListeners.		lastMouseEvent _ evt].	"Check for pending drag or double click operations."	mouseClickState ifNotNil:[		(mouseClickState handleEvent: evt from: self) ifFalse:[			"Possibly dispatched #click: or something and will not re-establish otherwise"			^self mouseOverHandler processMouseOver: lastMouseEvent]].	evt isMove ifTrue:[		self position: evt position.		self sendMouseEvent: evt.	] ifFalse:[		"Issue a synthetic move event if we're not at the position of the event"		(evt position = self position) ifFalse:[self moveToEvent: evt].		"Drop submorphs on button events"		(self hasSubmorphs) 			ifTrue:[self dropMorphs: evt]			ifFalse:[self sendMouseEvent: evt].	].	ShowEvents == true ifTrue:[self mouseFocus printString displayAt: (0@ofs) + (0@15)].	self mouseOverHandler processMouseOver: lastMouseEvent.! !!HandMorph methodsFor: 'private events' stamp: 'JMM 1/15/2007 11:09'!generateWindowEvent: evtBuf 	"Generate the appropriate window event for the given raw event buffer"	| evt |	evt := WindowEvent new.	evt setTimeStamp: evtBuf second.	evt timeStamp = 0 ifTrue: [evt setTimeStamp: Time millisecondClockValue].	evt action: evtBuf third.	evt rectangle: (Rectangle origin: evtBuf fourth @ evtBuf fifth corner: evtBuf sixth @ evtBuf seventh ).		^evt! !!MorphicEvent methodsFor: 'testing' stamp: 'JMM 10/6/2004 21:35'!isWindowEvent	^false! !!MorphicEvent methodsFor: 'private' stamp: 'ar 10/25/2000 20:53'!setTimeStamp: stamp	timeStamp := stamp.! !!MorphicEventDispatcher methodsFor: 'dispatching' stamp: 'bf 8/30/2007 20:30'!dispatchEvent: anEvent with: aMorph	"Dispatch the given event for a morph that has chosen the receiver to dispatch its events. The method implements a shortcut for repeated dispatches of events using the same dispatcher."	anEvent type == lastType ifTrue:[^self perform: lastDispatch with: anEvent with: aMorph].	"Otherwise classify"	lastType := anEvent type.	anEvent isMouse ifTrue:[		anEvent isMouseDown ifTrue:[			lastDispatch := #dispatchMouseDown:with:.			^self dispatchMouseDown: anEvent with: aMorph]].	anEvent type == #dropEvent ifTrue:[		lastDispatch := #dispatchDropEvent:with:.		^self dispatchDropEvent: anEvent with: aMorph].	anEvent isWindowEvent ifTrue:[		lastDispatch := #dispatchWindowEvent:with:.		^self dispatchWindowEvent: anEvent with: aMorph].	lastDispatch := #dispatchDefault:with:.	^self dispatchDefault: anEvent with: aMorph! !!MorphicEventDispatcher methodsFor: 'dispatching' stamp: 'bf 8/30/2007 20:29'!dispatchWindowEvent: anEvent with: aMorph	"Host window events do not have a position and are only dispatched to the World"	aMorph isWorldMorph ifFalse: [^#rejected].	anEvent wasHandled ifTrue:[^self].	^aMorph handleEvent: anEvent! !!PasteUpMorph methodsFor: 'event handling' stamp: 'bf 8/30/2007 17:59'!wantsWindowEvent: anEvent	^self isWorldMorph or: [self windowEventHandler notNil]! !!PasteUpMorph methodsFor: 'event handling' stamp: 'jmv 10/7/2010 11:38'!windowEvent: anEvent	self windowEventHandler		ifNotNil: [^self windowEventHandler windowEvent: anEvent].	anEvent type == #windowClose		ifTrue: [			^TheWorldMenu basicNew quitSession]! !!PasteUpMorph methodsFor: 'event handling' stamp: 'bf 8/30/2007 18:18'!windowEventHandler	"This is a class variable so it is global to all projects and does not get saved"	^WindowEventHandler! !!WindowEvent methodsFor: 'testing' stamp: 'JMM 10/6/2004 21:35'!isWindowEvent	^true! !!WindowEvent methodsFor: 'dispatching' stamp: 'bf 8/30/2007 17:31'!sentTo:anObject	"Dispatch the receiver into anObject"	^anObject handleWindowEvent: self.! !!WindowEvent methodsFor: 'accessing' stamp: 'JMM 10/6/2004 21:11'!action	^action! !!WindowEvent methodsFor: 'accessing' stamp: 'JMM 10/6/2004 21:10'!action: aValue	action := aValue.! !!WindowEvent methodsFor: 'accessing' stamp: 'JMM 10/6/2004 21:12'!rectangle	^rectangle! !!WindowEvent methodsFor: 'accessing' stamp: 'JMM 10/6/2004 21:12'!rectangle: aValue	rectangle := aValue.! !!WindowEvent methodsFor: 'accessing' stamp: 'bf 8/30/2007 20:42'!type	"This should match the definitions in sq.h"	^#(		windowMetricChange		windowClose		windowIconise		windowActivated		windowPaint	) at: action ifAbsent: [#windowEventUnknown]! !!classDefinition: #PasteUpMorph category: #'Morphic-Worlds'!BorderedMorph subclass: #PasteUpMorph	instanceVariableNames: 'cursor backgroundMorph worldState'	classVariableNames: 'DisableDeferredUpdates WindowEventHandler'	poolDictionaries: ''	category: 'Morphic-Worlds'!EventSensorConstants initialize!!classDefinition: #EventSensorConstants category: #'Kernel-Processes'!SharedPool subclass: #EventSensorConstants	instanceVariableNames: ''	classVariableNames: 'BlueButtonBit CommandKeyBit CtrlKeyBit EventKeyChar EventKeyDown EventKeyUp EventTypeDragDropFiles EventTypeKeyboard EventTypeMenu EventTypeMouse EventTypeNone EventTypeWindow OptionKeyBit RedButtonBit ShiftKeyBit YellowButtonBit'	poolDictionaries: ''	category: 'Kernel-Processes'!