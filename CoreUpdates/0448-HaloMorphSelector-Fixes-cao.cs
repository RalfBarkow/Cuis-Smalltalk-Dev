'From Cuis 2.0 of 24 February 2010 [latest update: #440] on 26 February 2010 at 10:34:56 pm'!!HaloMorph methodsFor: 'meta-actions' stamp: 'cao 2/26/2010 22:25'!blueButtonDown: event	"Transfer the halo to the next likely recipient"	target ifNil:[^self delete].	event hand obtainHalo: self.	positionOffset _ event position - (target point: target position in: owner).	"wait for drags or transfer"	event hand 		waitForClicksOrDrag: self 		event: event		clkSel: #transferHalo:		dblClkSel: nil		dblClkTimeoutSel: nil		dragSel: #dragTarget:! !