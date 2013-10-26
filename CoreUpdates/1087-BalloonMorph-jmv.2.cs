'From Cuis 3.3 of 2 June 2011 [latest update: #1024] on 23 September 2011 at 3:17:46 pm'!!BalloonMorph methodsFor: 'initialization' stamp: 'jmv 9/23/2011 14:26'!defaultBorderColor	"answer the default border color/fill style for the receiver"	^ "Color black alpha: 0.1"	BalloonColor alpha: 0.3! !!BalloonMorph methodsFor: 'initialization' stamp: 'jmv 9/23/2011 13:43'!defaultBorderWidth	"answer the default border width for the receiver"	^ 2! !!BalloonMorph methodsFor: 'menus' stamp: 'jmv 9/23/2011 15:16'!adjustedCenter	"This horizontal adjustment is needed because we want the interior TextMorph to be centered within the visual balloon rather than simply within the BalloonMorph's bounding box.  Without this, balloon-help text would be a bit off-center"	^ self referencePosition + (offsetFromTarget x sign * 14 @ 3)! !!BalloonMorph class methodsFor: 'utility' stamp: 'jmv 9/23/2011 14:35'!balloonColor	^ BalloonColor alpha: 0.8! !!BalloonMorph class methodsFor: 'private' stamp: 'jmv 9/23/2011 15:12'!getBestLocation: vertices for: morph corner: cornerName	"Try four rel locations of the balloon for greatest unclipped area.   12/99 sma"	| rect maxArea verts rectCorner morphPoint mbc a mp dir bestVerts result usableArea |	"wiz 1/8/2005 Choose rect independantly of vertice order or size. Would be nice it this took into account curveBounds but it does not." 	rect := Rectangle encompassing: vertices.  	maxArea := -1.	verts := vertices.	usableArea := (morph world ifNil: [self currentWorld]) viewBox.	1 to: 4 do: [:i |		dir := #(vertical horizontal) atWrap: i.		verts := verts collect: [:p | p flipBy: dir centerAt: rect center].		rectCorner := #(bottomLeft bottomRight topRight topLeft) at: i.		morphPoint := #(topCenter topCenter bottomCenter bottomCenter) at: i.		a := ((rect			align: (rect perform: rectCorner)			with: (mbc := morph displayBounds perform: morphPoint))				intersect: usableArea) area.		(a > maxArea or: [a = rect area and: [rectCorner = cornerName]]) ifTrue:			[maxArea := a.			bestVerts := verts.			mp := mbc]].	result := bestVerts collect: [:p | p + (mp - bestVerts first)] "Inlined align:with:".	^ result! !!BalloonMorph class methodsFor: 'private' stamp: 'jmv 9/23/2011 14:42'!getTextMorph: aStringOrMorph for: balloonOwner	"Construct text morph."	| m text fontToUse |	(aStringOrMorph is: #Morph)		ifTrue: [ m _ aStringOrMorph ]		ifFalse: [			text _ Text string: aStringOrMorph attribute: TextAlignment centered.			(fontToUse _ balloonOwner balloonFont)				ifNotNil: [					text font: fontToUse ].			m _ LimitedHeightTextMorph new				maxHeight: 500;				model: (TextModel new contents: text).			m textMorph paragraph composeAll ].	^ m! !!LimitedHeightTextMorph methodsFor: 'initialization' stamp: 'jmv 9/23/2011 15:16'!initialize	super initialize.	self basicExtent: 200 @ 120.! !!Preferences class methodsFor: 'bigger and smaller GUI' stamp: 'jmv 9/23/2011 13:58'!standardFonts	"Sets not only fonts but other GUI elements	to fit regular resolution and size screens	Preferences standardFonts	"		Preferences setDefaultFonts: #(		(setSystemFontTo: 'DejaVu' 9)		(setListFontTo: 'DejaVu' 9)		(setMenuFontTo: 'DejaVu' 10)		(setWindowTitleFontTo: 'DejaVu' 12)		(setBalloonHelpFontTo: 'DejaVu' 9)		(setCodeFontTo: 'DejaVu' 9)		(setButtonFontTo: 'DejaVu' 9)).	Preferences disable: #biggerCursors! !"Postscript:Leave the line above, and replace the rest of this comment by a useful one.Executable statements should follow this comment, and shouldbe separated by periods, with no exclamation points (!!).Be sure to put any further comments in double-quotes, like this one."Preferences standardFonts!