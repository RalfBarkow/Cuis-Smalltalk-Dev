'From Cuis 1.0 of 6 April 2009 [latest update: #169] on 12 April 2009 at 10:15:42 pm'!!PluggableListMorph methodsFor: 'event handling' stamp: 'jmv 4/12/2009 22:13'!mouseEnter: event	super mouseEnter: event.	Preferences focusFollowsMouse		ifTrue: [ event hand newKeyboardFocus: self ]! !!PluggableTextMorph methodsFor: 'event handling' stamp: 'jmv 4/12/2009 22:13'!mouseEnter: event	super mouseEnter: event.	selectionInterval ifNotNil:		[textMorph editor selectInterval: selectionInterval; setEmphasisHere].	textMorph selectionChanged.	Preferences focusFollowsMouse		ifTrue: [ event hand newKeyboardFocus: textMorph ]! !!Preferences class methodsFor: 'standard queries' stamp: 'jmv 4/12/2009 22:15'!focusFollowsMouse	^ self		valueOfFlag: #focusFollowsMouse		ifAbsent: [ true ]! !!SimpleHierarchicalListMorph methodsFor: 'event handling' stamp: 'jmv 4/12/2009 22:14'!mouseEnter: event	super mouseEnter: event.	Preferences focusFollowsMouse		ifTrue: [ event hand newKeyboardFocus: self ]! !