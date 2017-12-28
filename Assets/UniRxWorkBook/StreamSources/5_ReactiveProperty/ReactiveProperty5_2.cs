using System;
using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

namespace UniRxWorkBook.StreamSources
{

    public class ReactiveProperty5_2 : MonoBehaviour
    {
        [SerializeField]
        private Text subscribeText; //RPをSubscribeした結果を表示する
        [SerializeField]
        private Text updateText;    //Updat内でRPを監視して表示する
        [SerializeField]
        private Text toReactivePropertyText; //ストリームをRPに変換して表示する

        private int seconds = 5;
        // ReactiveProperty(以下RP）は、普通の変数にSubjectの機能を付け加えたものである
        // 要するに、「Subscribeができる変数」である。挙動としてはBehaviorSubjectに似ている
        //
        // RPにはValueというプロパティが用意されており、このValueに値を代入することでOnNextを発行することができる
        // また、このValueはSubscribeなしで値にアクセス可能である（つまりSubscribeせずに普通の変数のようにReadができる）
        private ReactiveProperty<float> reactiveProperty = new ReactiveProperty<float>(0);

        /// <summary>
        /// 一時停止フラグ
        /// </summary>
        public bool IsPaused { get; private set; }
        private void Start ( )
        {

            //RPをSubscribeしてsubscribeTextに反映してみるパターン
            //reactiveProperty.Subscribe( value => subscribeText.text = value.ToString() );

            //-------------------

            reactiveProperty.Value = seconds;

            //RPのValueを毎フレームReadしてupdateTextに反映してみるパターン
            //this.UpdateAsObservable()
            //    .Where( _ => reactiveProperty.Value >=0 )
            //    .DoOnCompleted(()=>Debug.Log("ss"))
            //    .Subscribe(
            //    _ => {
            //        TimeSpan ts = TimeSpan.FromSeconds( reactiveProperty.Value );
            //        //RPのValueプロパティでいつでも最新の値が取得できる
            //        updateText.text = string.Format( "{0:00}:{1:00}:{2:00}" ,
            //            ts.Minutes , ts.Seconds , ts.Milliseconds );
            //        Debug.Log( reactiveProperty.Value );
            //        reactiveProperty.Value -= Time.deltaTime;
            //    });



            //-------------------

            //60秒カウントするストリームをコルーチンから作る
            var isdf = Observable.FromCoroutine<int>( observer => GameTimerCoroutine( observer , seconds ) )
                .Subscribe(
                t => updateText.text = t.ToString(), ( ) => Debug.Log( "ss" ) );

            
            //-------------------

            //他のストリームをReactivePropertyに変換してみるパターン
            //var interbalRP
            //    = Observable.Timer(DateTimeOffset.Now,TimeSpan.FromSeconds(1)) //1秒毎にカウントアップ
            //    .ToReactiveProperty(0); //RPに変換(初期値は０）
            //interbalRP.Subscribe( value => toReactivePropertyText.text = value.ToString() )
            //    .AddTo( this ); //GameObject破棄時にDisposeする

            //-------------------

            //RPの更新開始
            //StartCoroutine( CountUpCoroutine() );
        }

        /// <summary>
        /// 初期値から0までカウントするコルーチン
        /// ただしIsPausedフラグが有効な場合はカウントを一時停止する
        /// </summary>
        private IEnumerator GameTimerCoroutine ( IObserver<int> observer , int initialCount )
        {
            var current = initialCount;
            while ( current > 0 )
            {
                if ( !IsPaused )
                {
                    observer.OnNext( current-- );
                }
                yield return new WaitForSeconds( 1 );
            }
            observer.OnNext( 0 );
            observer.OnCompleted();
        }

        //RPを１秒毎に更新する（カウントアップする）
        private IEnumerator CountUpCoroutine ( )
        {
            yield return new WaitForSeconds( 1 );
            while ( true )
            {
                reactiveProperty.Value++;
                yield return new WaitForSeconds( 1 );
            }
        }


    }
}
