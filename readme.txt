
===================================================================================================

Assets
  │
  │
  └──Scenes
	│
	├──Refactoring(リファクタリングの授業で使う物が入っている。)
  	│	
  	├──UnityGames(ここにゲーム制作の授業で作った課題を)
  	│　 ├──Coinpusher
	│　 ├──WhackAMole
	│　 ├──TurnBattle
	│　 ├──CardGame
	│　 ├──RTS
	│　 │　　
	│　 └──???
	│
  	└──Photon(ネットワーク)
	

上記のようなフォルダ階層になっております。
===================================================================================================


以下はゲーム制作の欄


-----------------------------------Coin pusher----------------------------------------------------

Playerのコインの所持数をReactiveProperty<int>にしておくことで、スムーズにUIの表示を行えることが出来ました

コインを押す台は、Sinは使わずDotweenを使って書いたので、直感的にどこからどこまで動くのか？を直感的に操作が出来るので、短い開発時間の中を上手く動作することが可能に。

コインが0枚以下になったら...という所もちゃんとできているので、-1...-2...などとなって行かないように調整済みです。

全てのオブジェクトをProBuilderで作りました。

-----------------------------------WhackAMole----------------------------------------------------

連打防止のためモグラの叩く判定をUniRXで設計しました。

著作権の関係上、publicリポジトリにエフェクトのアセットはpushしていませんが、表示用の実装をしてリッチな見た目になる様に設計しました。

モグラの出現量を変更するためにデータテーブルで管理された値を使用する事で、後々の時間による難易度調整がスムーズになります。

正直WhackAMoleValueManagerはSingletonとかにした方が買ったかもと反省点はありますが、

3*3でも4*4でも5*5でも [ContextMenu("タイルを取得")]で取得するので、タイルがどれだけ離れていても、どれだけあっても取得できるようになっています。

-----------------------------------TurnBattle----------------------------------------------------

UniRXを使用、MV(R)PパターンとしてUI周りを設計しました、

[Player]と[Enemy]を【Character】クラスの子クラスとしたことで、ロジックの共通部分などをまとめる事が可能になりました。

なのでパラメーターを変更などをすれば他のキャラクターを作れる拡張性の高いキャラクターの制作を目指しました。

あとUI部分はCanvasGroupでまとめたので変更などもやり易くなりました。

-----------------------------------CardGame----------------------------------------------------

ソートの機能を実装するときに、自分のEnum型ソートオプションをUniRXでSubScribeして、

sortOption.Subscribe(_ =>
{
    cardsTrans = GetSortAction();
}
List<Transform> GetSortAction()
{
    return _sortOption.Value switch
    {
        CardSortOption.Cost => _cardsHandlerList.OrderBy(card => card.CardData.cost).Select(kv => kv.transform).ToList(),

        CardSortOption.HP => _cardsHandlerList.OrderBy(card => card.CardData.hp).Select(kv => kv.transform).ToList(),

        CardSortOption.Count => _cardsHandlerList.OrderBy(card => card.CardData.cardsCount).Select(kv => kv.transform).ToList(),

        _ => _cardsHandlerList.OrderBy(card => card.CardData.cost).Select(kv => kv.transform).ToList(),
    };
}

と個人的に好みであるswitchの書き方 × LinQを使用したソート方法で分かりやすいコーディングができたかなと思っています、

GridLayoutGroupで位置を設定しているため、1フレーム待たなきゃいけないなどのちょっと苦戦した部分がありましたが今回はすんなりと完成出来ました。


-----------------------------------RTS-------------------------------------------------------

リアルタイムストラテジー風にNaviMeshAgentsを使ってユニットを選択して動かすプログラムを組みました。

選択方法は範囲選択、もしくは単一選択でユニットを選ぶことが出来ます。

Start関数に初期化処理とマウスをクリックしたときの処理を纏めたので可読性が高いプログラムを組めたと自負しております。。

private void Start()
{
    InitializeAgents();　//初期化処理
    this.UpdateAsObservable().Subscribe(_ =>
    {
        if (!_isSelect)
        {
            if (Input.GetMouseButtonDown(0))
                SelectCharacterStart(); //位置記録開始
            else if (Input.GetMouseButtonUp(0))
                SelectCharacterEnd();	//位置記録終了
        }
        else if (Input.GetMouseButtonDown(0))
            SelectGroundPosition();	//移動先選択
    });
}

また、選択した範囲が閾値以下の場合には単一選択をしているのだと見なしRayCastを飛ばし選択するのでユニット選択漏れが少ないプログラムが組めています。

-----------------------------------？？？？？？----------------------------------------------------


-----------------------------------？？？？？？----------------------------------------------------
