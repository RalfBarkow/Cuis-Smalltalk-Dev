'From Cuis 2.7 of 3 September 2010 [latest update: #600] on 15 October 2010 at 9:38:04 am'!!DisplayScanner methodsFor: 'scanning' stamp: 'jmv 10/15/2010 08:49'!displayBulletOffset: offset number: bulletNumber	| pattern i c j s bullet bulletPos bulletSize prefix |	pattern _ actualTextStyle listBulletPattern.	bullet _ pattern.	(i _ pattern indexOf: $%) > 0		ifTrue: [ bullet _ bulletNumber asString]		ifFalse: [			(i _ pattern indexOf: $z) > 0				ifTrue: [ bullet _ (Character value: 96 + bulletNumber) asString ]				ifFalse: [					(i _ pattern indexOf: $Z) > 0						ifTrue: [ bullet _ (Character value: 64 + bulletNumber) asString ]]].	prefix _ 0.	i > 0 ifTrue: [		c _ pattern at: i.		j _ i.		s _ pattern size.		[ j <= s and: [ (pattern at: j) = c ] ] whileTrue: [ j _ j + 1 ].		j _ j - 1.		bulletSize _ j-i+1.		prefix _ bulletSize - bullet size max: 0.		bullet size > bulletSize ifTrue: [			bullet _ bullet copyFrom: bullet size - bulletSize + 1 to: bullet size ].		bullet _ (pattern copyFrom: 1 to: i-1), bullet, (pattern copyFrom: j+1 to: pattern size) ].	bulletPos _ actualTextStyle firstIndent + offset x + ((font widthOf: $9) * prefix)@destY.	font displayString: bullet on: bitBlt from: 1 to: bullet size at: bulletPos kern: kern! !!TextStyle class methodsFor: 'examples' stamp: 'jmv 10/15/2010 08:52'!createExamples	"	TextStyle createExamples	"	| dejaVu22 dejaVu17 dejaVu14 dejaVu11 dejaVu10 heading1 heading2 heading3 emphasized normal numbered alphabetic bulleted |	dejaVu22 _ AbstractFont familyName: 'DejaVu' pointSize: 22.	dejaVu17 _ AbstractFont familyName: 'DejaVu' pointSize: 17.	dejaVu14 _ AbstractFont familyName: 'DejaVu' pointSize: 14.	dejaVu11 _ AbstractFont familyName: 'DejaVu' pointSize: 11.	dejaVu10 _ AbstractFont familyName: 'DejaVu' pointSize: 10.		heading1 _ TextStyle withFont: dejaVu22 name: 'Heading 1' alignment: CharacterScanner centeredCode.	heading1		privateParagraphSpacingBefore: 34;		privateParagraphSpacingAfter: 18.	self makeAvailable: heading1.	heading2 _ TextStyle withFont: dejaVu17 bold name: 'Heading 2' alignment: CharacterScanner leftFlushCode.	heading2		privateParagraphSpacingBefore: 24;		privateParagraphSpacingAfter: 8.	self makeAvailable: heading2.	heading3 _ TextStyle withFont: dejaVu14 italic name: 'Heading 3' alignment: CharacterScanner leftFlushCode.	heading3		privateParagraphSpacingBefore: 18;		privateParagraphSpacingAfter: 4.	self makeAvailable: heading3.	emphasized _ TextStyle withFont: dejaVu10 bold name: 'Emphasized' alignment: CharacterScanner justifiedCode.	emphasized		privateFirstIndent: 60;		privateRestIndent: 60;		privateRightIndent: 60;		privateParagraphSpacingBefore: 10;		privateParagraphSpacingAfter: 2.	self makeAvailable: emphasized.		normal _ TextStyle withFont: dejaVu11 name: 'Normal' alignment: CharacterScanner justifiedCode.	normal		privateFirstIndent: 30;		privateRestIndent: 10;		privateRightIndent: 10;		privateParagraphSpacingBefore: 8;		privateParagraphSpacingAfter: 2.	self makeAvailable: normal.		numbered _ TextStyle withFont: dejaVu11 name: 'Numbered List' alignment: CharacterScanner justifiedCode.	numbered		privateFirstIndent: 10;		privateRestIndent: 50;		privateRightIndent: 10;		privateParagraphSpacingBefore: 8;		privateParagraphSpacingAfter: 2;		privateListBulletPattern: '%%%. '.	self makeAvailable: numbered.		alphabetic _ TextStyle withFont: dejaVu11 name: 'Alphabetic List' alignment: CharacterScanner justifiedCode.	alphabetic		privateFirstIndent: 10;		privateRestIndent: 30;		privateRightIndent: 10;		privateParagraphSpacingBefore: 8;		privateParagraphSpacingAfter: 2;		privateListBulletPattern: 'z) '.	self makeAvailable: alphabetic.		bulleted _ TextStyle withFont: dejaVu11 name: 'Bulleted List' alignment: CharacterScanner justifiedCode.	bulleted		privateFirstIndent: 10;		privateRestIndent: 30;		privateRightIndent: 10;		privateParagraphSpacingBefore: 8;		privateParagraphSpacingAfter: 2;		privateListBulletPattern: '� '.	self makeAvailable: bulleted.! !"Postscript:Leave the line above, and replace the rest of this comment by a useful one.Executable statements should follow this comment, and shouldbe separated by periods, with no exclamation points (!!).Be sure to put any further comments in double-quotes, like this one."	TextStyle createExamples!