'From Cuis 2.9 of 5 November 2010 [latest update: #634] on 15 December 2010 at 11:01:19 pm'!!TextModelMorph methodsFor: 'menu commands' stamp: 'jmv 12/15/2010 22:59'!cancel	model refetch.	self maybeStyle.	self setSelection: model getSelection.	self hasUnacceptedEdits: false ! !