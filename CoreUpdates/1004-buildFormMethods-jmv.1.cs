'From Cuis 3.2 of 12 April 2011 [latest update: #914] on 25 May 2011 at 2:39:10 pm'!!CursorWithAlpha class methodsFor: 'constants' stamp: 'jmv 5/25/2011 14:36'!biggerNormal	"self biggerNormal show"	^self constants at: #biggerNormal ifAbsentPut: [		| form cursor |		form _ self buildBiggerNormal.		cursor _ CursorWithAlpha extent: form extent depth: 32.		form displayOn: cursor.		cursor offset: -2@-1.		cursor preMultiplyAlpha.		cursor]! !!CursorWithAlpha class methodsFor: 'constants' stamp: 'jmv 5/25/2011 14:36'!buildBiggerNormal	"(Base64MimeConverter mimeEncode: ((FileStream readOnlyFileNamed: 'EtoysCursor.png') binary)) upToEnd"	^Form fromBinaryStream: 		'iVBORw0KGgoAAAANSUhEUgAAABsAAAArCAYAAACJrvP4AAAACXBIWXMAAAsTAAALEwEAmpwY			AAAEF0lEQVRYCb2XS0hcVxjHj2/G8ZWMQQbSGO2iRhERwRALBmJNoOOqwUXtYtxOTQJxI8SN			CyXJqggVEghusrAuBB8IddUWF64CvnCj+AJrsYKio6JJOPn/j+dc5s6MztyZMR/857v3PL7f			/c4595w7QmiTUvrN9ZV7wGhfB3jOunpgOoYtPQQm19fXn6DsulY2PJUSi4ARvLm5+SuiE5hS			mAsBXSYzv99vLuXExMRL1H2jlRKoDYbAMhS4uLj4PJUwN4K5TTqEhQPHxsZeayCzTCrDqLC0			tLQryTAqjNmFA1OR4YWwaMBk5/BSWDRgMhnGhEUDJpphXDACqdDXIpEMHcHCF43TDB3Bks0w			IVj4kMabYcKwcGA8c+gIlp2drRaKGc5wYKwMHcFycnIiYOHACzLkhi9SAgsHRsnQOSzaMBJk			FPoejo6OvkJ5iZY67R1lZoJe5kOBKysrzxzBCgoKrCcnpKysTO7v75sjMKafmZl5gX6uNPww			M4EeQXrsEAJDJc7Ozngr8vPzRVVVldjZ2RGrq6uqrLi4WPT394u2tjZxeHj4P8C7qiLkJzMz			8zNvc3NzT+jR/yl9xDBmZWWpTAoLC2V9fb3c29uTXV1dtuwaGxtVRgcHBzuI0QY91vLBUw+0			voOnXPyyijBEUWWVlZViampKFBUVCcyDKC8vt9pitYnp6WlmfqO7u/uOVRHjIiKzjIwM2dDQ			oDIKnZCWlhZbdoFAQFUvLCz8Bcb3WrfgqWItFR/XKrEIWG1trQWam5v7Z3Bw8C2jjoyMyNLS			UgvIYeYQ05A5h5HA+GE1NTVWgPn5+b/RubWiosJ/enoaZNDq6moLhjrZ19fHYjk7O/sO9/eg			G1oZ8JTNbJmZJ9Wgn9GyleJQMWhPT48NhnllsTw+Pv4X7WLCuI1YX8TsuLy8/CfKmrXuwt9t			b2//iXX4LJder9cCut1uOT4+zio5PDz8G9pWaqm4uLaZDaZBXLY2GO4bdnd3PzAowDZYc3Mz			i+X29vY82l0K4ypR/2JOTk7e49qsIuMLUEbdXFpaes6gk5OT0uPxWECeBGtra6ySvb29v6Bt			ve7DfjZTsKOjo99RyvkzEOMtGOpuBoPBbQblQsK9Ejfnzs5OFsuNjY0JlF8IQ11clodWeVgo			bxh0YGDABmOmNGxzh2j3EPJqRV2VqLvUFKyjo+NHBuWqxb4nS0pKVFZmGFG+gihJw8wTerHx			/kEgXng6y7a2thYxnAHAHkHfavEcoxyZBcOh+AOHixS+7HwnfT4f/6nynSQoaZh5MjWcTU1N			94aGhtrr6up8qLgPcVFQd7SuwVPmIdN5njk1wmi31a8QHu3VuYVrLhDaf+dOHGgvE4Gp3RsB			cnUQMx+f9P1H7c9PXyHUIcoy01HXX637AibwgHAnFRPGAAAAAElFTkSuQmCC'				base64Decoded asByteArray readStream! !!SketchMorph class methodsFor: 'accessing - icons' stamp: 'jmv 5/25/2011 14:35'!buildPaintingIcon	"Created using:	Clipboard default storeObject:	 	((PNGReadWriter bytesFor: (aForm)) asString base64Encoded)	"	^ Form fromBinaryStream: 'iVBORw0KGgoAAAANSUhEUgAAACkAAAA0CAYAAAAXKBGzAAABgElEQVR4XuWZP5KCMBTGU1p6BEtLSkpLj7ClJUfwCJaWW3oES46w5R7B0tLSUj9nGLMxgUjeP1xmvhEI4G++l/cIifvobbfKl1kwcdASOBHQXIBDpQSa86e/c+cus78aAlYHFAPNDXEKEGoXzKGXghwN+k629oWbLePDBzR17ZZV62bV9SHs45x/DRwb4yAJJGA6OF8ALSlBpJAxwCFQdsgcF01BQn5fDLWutzYgw6Qx52IIC+egMLNFBxpUzrAOMMxDph62+ro3bp/CsSlIH87XojES6tDBUCWOkkHCLS5IsnBv1v2QaBdPotjNAAkdxTEl4FvAfTcitJ1US5RkjRwNahHyBXTo4uP+KTXQPrjT+VXfjQJorAEgMUAN0KSbKRc7SYd+upDQTxsHxHmVBEo1ou/BNYDht6QvlpQ81lpJNeBmK+qUXwZmZnPJ3j7cs7ifA6m9wiAyfPu3kCwTBiIuaoCyfKhRgorMupXATnrmTWyNx8RCqXlADuDJL9jfAEQTbn3reyhsAAAAAElFTkSuQmCC' base64Decoded asByteArray readStream! !!SketchMorph class methodsFor: 'accessing - icons' stamp: 'jmv 5/25/2011 14:35'!paintingIcon	^ PaintingIcon ifNil: [		PaintingIcon _ self buildPaintingIcon ]! !!ColorPickerMorph class methodsFor: 'accessing - icons' stamp: 'jmv 5/25/2011 14:37'!buildEyedropperIcon	"Created using:	Clipboard default storeObject:	 	((PNGReadWriter bytesFor: aForm) asString base64Encoded)	"	^ Form fromBinaryStream: 'iVBORw0KGgoAAAANSUhEUgAAACYAAAAkCAYAAADl9UilAAABiklEQVR4XuXYoZKDMBAGYO6ZYpBIJBKJYngTHoHJEyCRlUhMfCWyEonc5l+aTI62c+qy6TQzO7HfbDY/abPvX5+yjNZk2paMUqTtngbKQsiCiPcHzpYsykIYdCrgWimcCTH2KH05WJZR13UUGaX52F6hQhgqKszP1BsUZkwx6oeSQrUe9Y8wjoFH/XV80VDHzVMcB8f+AqUEUFj6FAO+Y4/8ctGgYqI8DoAA5+q5SxFR4zjSPM+MCGdJFDVNE63rSljLsjDmOQ4EUNu2MWrfd7per5zk6JJGakjMVIi63W5kjD1FO/DKdqrve96joy6XC3foF8p+gvI89wjumMdFRKGA0lYFVFmWcs+Yc6eAwkwpybcV4sANOaO0obquqSgK2U6FN8+hmqaRQyGbzijMk+hMhSjcPIdKolNhRgEV/Sn8aqZCFDJKtFNI9HNGAYU0F0PhlYAPsosDdAhxIIpqzfE6cCjMU1VV8r+YAUMcuM/LMAxp/IwHDEcmeutCzLtK5t+YpDDfD/vkdQfdhqlIh/LZTwAAAABJRU5ErkJggg==' base64Decoded asByteArray readStream! !!ColorPickerMorph class methodsFor: 'accessing - icons' stamp: 'jmv 5/25/2011 14:38'!eyedropperIcon	^ EyedropperIcon ifNil: [		EyedropperIcon _ self buildEyedropperIcon ]! !