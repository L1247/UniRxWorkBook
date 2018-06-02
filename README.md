##UniRx問題集

各Scriptの____を書き換え、正しく動くようにしてみよう。  
UniRxは同梱されていません。  


MITライセンスで公開します。  

##使用ライセンス
UniRx問題集はUniRxを使用しています
  
Copyright (c) 2014 Yoshifumi Kawai https://github.com/neuecc/UniRx/blob/master/LICENSE

# Operator 一覽表
## Factory Method
想做的事情 | Operator | 備註
--- | --- | ---
*Still* | `renders` | **nicely**
1 | 2 | 3

## Message Filter
想做的事情 | Operator | 備註
--- | --- | ---
滿足條件式才通過 | Where | 在別的語言中稱作filter
去除已重複的東西|	Distinct |
只有在數值變化的時候才通過 |	DistinctUntilChanged	
在指定的時間內只有最後一個訊息會通過 (OnNext)  | Throttle*/ThrtottleFrame*	
在指定的時間內只有最初的訊息會通過 (OnNext) | ThrottleFirst*/ThrottleFirstFrame*	
只讓Stream當中的第一個到達的OnNext通過 |	First/FirstOrDefault	
OnNext兩個以上被通過時，則出現錯誤 |	Single/SingleOrDefault	
只讓 Steam 最後的值通過 |	Last/LastOrDefault	
只讓指定的個數數量通過 |	Take	
指定的條件不成立前都通過 |	TakeWhile	
指定的Stream的OnNext之前都通過 |	TakeUntil
指定的數量完成以前都跳過 |	Skip	
指定的條件成立後通過 |	SkipWhile	
指定的Stream的OnNext通過之前都不通過 |	SkipUntil	
型別一致才通過 |	OfType<T>	
OnError 或者 OnCompleted 被呼叫時通過 |	IgnoreElements
