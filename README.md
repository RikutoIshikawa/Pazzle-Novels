# Pazzle-Novels
 
## １.システムの概要
自作ゲーム **「Pazzle&Novels」** のUnityプロジェクトです。

## ２.ゲーム概要
①ゲーム名：Pazzle&Novels

②プラットフォーム：PC

③ゲームジャンル：ノベル×パズル

④ゲームコンセプト：突如目覚めたパズルの力で、ユニティちゃんたちを襲うクリーチャーを倒し、

　　　　　　　　　　ストーリーを進めていく異世界アドベンチャーゲームです。
          
　　　　　　　　　　あなたもゲームを通して、非日常的な体験を味わいましょう！

⑤使用技術：Unity C#、VisualStadio2022

⑥製作期間：10日

## ３.ゲームルールと操作方法
使用するのは、マウスのみです。
### ◎ノベルゲーム部分
①ゲームルール

　ノベルゲーム式に文章を読み進めていく物語パートです。

②操作方法

　画面をクリックすることで、テキストボックス内のシナリオが進み、

　シナリオが進むごとに背景やキャラクター画像が切り替わっていきます。

### ◎パズルゲーム部分
①ゲームルール

　物語パートの最中に発生するバトルパートです。

　ゲーム開始時は、プレイヤーとクリーチャーにはそれぞれ初期HPが設定されており、

　４種類のブロックがランダムに５０個振ってきます。

　ブロックは同じ種類を３つ以上繋げることで、消すことができます。

　そして、消した個数分だけ、種類はランダムに上から補充されていきます。

　ここでゲームのポイントとして、消したブロックの種類により、

　プレイヤーもしくはクリーチャーのHPが、消した数だけ増減していきます。

　具体的には、
 
　　**・剣ブロックの場合：「消した個数×３０」だけ敵にダメージを与える。**
  
　　**・お玉ブロックの場合：「消した個数×１０」だけ敵にダメージを与える。**
  
　　**・ハートブロックの場合：「消した個数×２０」だけプレイヤーが回復する。**
  
　　**・ドクロブロックの場合：「消した個数×１５」だけプレイヤーがダメージを受ける。**
  
　このようにして、バトルシステムを実現しています。

②操作方法

　クリックしてブロックを選択し、クリックしたまま同じ種類のブロックを繋げて、

　クリックを離すことで繋げたブロックを消すことができます。

## ３.シーン構成
　①Title_Scene：タイトル
 
　②Main_Scene：物語１（分岐点：Yes/No）

　③Game_Over1：ゲームオーバー１（Noの場合）
 
　④Middle_Scene：物語２（Yesの場合）

　⑤Pazzle_Scene：パズルバトル（分岐点：勝利/敗北）

　⑥Game_Over2：ゲームオーバー２（敗北の場合）

　⑦Final_Scene：物語３（勝利の場合）

## ４.スクリプト概要
### ◎ノベルゲーム部分
①GameManager

　ノベルゲームの各スクリプトの管理を行うスクリプト
 
②ScenarioManager

　シナリオの管理を行うスクリプト
 
③MaterialChange

　シナリオの命令に応じて、背景やキャラクター画像の切り替え、BGMの切り替え、パネルの
　切り替えなどを行うスクリプト
 
④BG_Operation

　Background（背景画像）を管理するスクリプト
 
⑤Item_Operation

　Item（クリーチャー画像、魔法陣、扉など）を管理するスクリプト
 
⑥Character_Operation

　Character（キャラクター画像）を管理するスクリプト
 
### ◎パズルゲーム部分
①Block

　Block（パズルブロック）の情報を管理するスクリプト
 
②Pazzle_GameManager

　パズルゲームのゲーム管理を行うスクリプト
 
③BlockGenerator

　Block（パズルブロック）を生成するスクリプト
 
④ParamsSO

　HP、初期ブロック数、ブロックによるHPの増減値など、パズルゲームに関するパラメータを
 
　操作できるスクリプト
 
### ◎共通部分
①ChangeScene

　シーン切り替え関数を管理するスクリプト
 
②SoundManager

　BGMやSEの管理をするスクリプト

## ５.オブジェクト概要
### ◎Title_Scene

①Background：背景画像

②TitleLogo：タイトルロゴ

③Title_Text：操作指示文

④Start_Button：スタートボタン（押すとMain_Sceneに移動する）

⑤SoundManager：SoundManagerスクリプト用の空オブジェクト

### ◎Main_Scene
①GameBox：ノベルゲーム画面

　・Background：背景画像
 
　・Gallery：イベントCG
 
　・Item：アイテム（クリーチャー画像、魔法陣、扉など）
 
　・Character：キャラクター画像
 
　・TextBox：シナリオを表示する枠
 
　　→Scenario：シナリオ（外部テキスト）
  
②ButtonBox：分岐選択画面

　・Yes_Button：Yesボタン（押すとMiddle_Sceneに移動する）
 
　・No_Button：Noボタン（押すとGame_Over1に移動する）
 
③GameManager：GameManagerスクリプト用の空オブジェクト

④MaterialChange：MaterialChangeスクリプト用の空オブジェクト

⑤BG_Operation：BG_Operationスクリプト用の空オブジェクト

⑥Item_Operation：Item_Operationスクリプト用の空オブジェクト

⑦Character_Operation：Character_Operation用の空オブジェクト

⑧SoundManager：SoundManagerスクリプト用の空オブジェクト

### ◎Game_Over1
①GameBox：ノベルゲーム画面

　・Background：背景画像
 
　・Gallery：イベントCG
 
　・Item：アイテム（クリーチャー画像、魔法陣、扉など）
 
　・Character：キャラクター画像
 
　・TextBox：シナリオを表示する枠
 
　　→Scenario：シナリオ（外部テキスト）
  
②ButtonBox：ゲームオーバー画面

　・Retry_Button：リトライボタン（押すとMain_Sceneに移動する）
 
　・Title_Button：タイトルに戻るボタン（押すとTitle_Sceneに移動する）
 
③GameManager：GameManagerスクリプト用の空オブジェクト

④MaterialChange：MaterialChangeスクリプト用の空オブジェクト

⑤BG_Operation：BG_Operationスクリプト用の空オブジェクト

⑥Item_Operation：Item_Operationスクリプト用の空オブジェクト

⑦Character_Operation：Character_Operation用の空オブジェクト

⑧SoundManager：SoundManagerスクリプト用の空オブジェクト

### ◎Middle_Scene
①GameBox：ノベルゲーム画面

　・Background：背景画像
 
　・Gallery：イベントCG
 
　・Item：アイテム（クリーチャー画像、魔法陣、扉など）
 
　・Character：キャラクター画像
 
　・TextBox：シナリオを表示する枠
 
　　→Scenario：シナリオ（外部テキスト）
  
②RuleBox：パズルゲームルール説明画面

　・Ready_Button：レディボタン（押すとPazzle_Sceneに移動する）
 
③GameManager：GameManagerスクリプト用の空オブジェクト

④MaterialChange：MaterialChangeスクリプト用の空オブジェクト

⑤BG_Operation：BG_Operationスクリプト用の空オブジェクト

⑥Item_Operation：Item_Operationスクリプト用の空オブジェクト

⑦Character_Operation：Character_Operation用の空オブジェクト

⑧SoundManager：SoundManagerスクリプト用の空オブジェクト

### ◎Pazzle_Scene
①GameBox：パズルゲーム画面

　・Background：背景画像
 
　・EnemyHPTextBox：クリーチャーのステータス画面
 
　　→EnemyName：クリーチャー名
  
　　→EnemyHP：クリーチャーのHP値
  
　　→EnemyHPSlider：クリーチャーのHPバー
  
　・PlayerHPTextBox：プレイヤーのステータス画面
 
　　→EnemyName：プレイヤー名
  
　　→EnemyHP：プレイヤーのHP値
  
　　→EnemyHPSlider：プレイヤーのHPバー
  
　・OffensivePowerFrame：ブロックの効果説明画面
 
　・Boad_Frame：ブロックが生成される枠
 
　・WallBox：ブロックが枠に収まるようにするコライダー　
 
②ClearPanel：クリア画面

　・ClearText：クリア文字
 
　・Next_Button：次へボタン（押すとFinal_Sceneへ移動）
 
③BlockGenerator：BlockGeneratorスクリプト用の空オブジェクト

④PazzleGameManager：PazzleGameManagerスクリプト用の空オブジェクト

⑤SoundManager：SoundManagerスクリプト用の空オブジェクト

### ◎Game_Over2
①GameBox：ノベルゲーム画面

　・Background：背景画像
 
　・Gallery：イベントCG
 
　・Item：アイテム（クリーチャー画像、魔法陣、扉など）
 
　・Character：キャラクター画像
 
　・TextBox：シナリオを表示する枠
 
　　→Scenario：シナリオ（外部テキスト）
  
②ButtonBox：ゲームオーバー画面

　・Retry_Button：リトライボタン（押すとPazzle_Sceneに移動する）
 
　・Title_Button：タイトルに戻るボタン（押すとTitle_Sceneに移動する）
 
③GameManager：GameManagerスクリプト用の空オブジェクト

④MaterialChange：MaterialChangeスクリプト用の空オブジェクト

⑤BG_Operation：BG_Operationスクリプト用の空オブジェクト

⑥Item_Operation：Item_Operationスクリプト用の空オブジェクト

⑦Character_Operation：Character_Operation用の空オブジェクト

⑧SoundManager：SoundManagerスクリプト用の空オブジェクト

### ◎Final_Scene
①GameBox：ノベルゲーム画面

　・Background：背景画像
 
　・Gallery：イベントCG
 
　・Item：アイテム（クリーチャー画像、魔法陣、扉など）
 
　・Character：キャラクター画像
 
　・TextBox：シナリオを表示する枠
 
　　→Scenario：シナリオ（外部テキスト）
  
②FinalBox：ラスト画面

　・Charater：キャラクター画像
 
　・Title_Button：タイトルボタン（押すとTitle_Sceneに移動する）
 
　・Message：ゲーム終了メッセ―ジ
 
③GameManager：GameManagerスクリプト用の空オブジェクト

④MaterialChange：MaterialChangeスクリプト用の空オブジェクト

⑤BG_Operation：BG_Operationスクリプト用の空オブジェクト

⑥Item_Operation：Item_Operationスクリプト用の空オブジェクト

⑦Character_Operation：Character_Operation用の空オブジェクト

⑧SoundManager：SoundManagerスクリプト用の空オブジェクト

## ６.使い方
一部画像（背景画像、キャラクター画像）やBGM、SEなど設定されていないため、下記の手順で必要素材を挿入してください。

### ◎Title_Scene
①Backgroundに背景画像を挿入する。

②SoundManagerにBGMを挿入する。（計５個）

　・StartBGM：タイトルシーンで流れるBGM
 
　・LifeBGM：ノベルシーンで流れるBGM
 
　・UnrestBGM：ノベルシーンで流れるBGM２
 
　・BattleBGM：パズルシーンで流れるBGM
 
　・FinalBGM：ノベルシーンで流れるBGM
 
③SoundManagerにSEを挿入する。（計１０個）

　・StartButton：スタートボタンを押した音
 
　・Button：ボタンを押した音
 
　・Block：ブロックに触れた音
 
　・Sword：剣の攻撃音
 
　・Ladle：お玉の攻撃音
 
　・Heart：ハートの回復音
 
　・Skull：毒のダメージ音
 
　・LastAttack：最後の攻撃音
 
　・Victory：勝利音
 
　・Creature：クリーチャーの鳴き声

### ◎Main_Scene

①Yes_ButtonとNo_Buttonにボタン画像を挿入する。

②BG_Operationに背景画像等を挿入する。

　・Background：背景画像
 
　・Gallery1：イベントCG（外形画像の暗闇Var画像や薄黒い画像）
 
③Item_Operationにアイテム画像を挿入する。

　・Item1：魔法陣の画像
 
　・Item2：クリーチャーの画像
 
④Character_Operationにキャラクター画像を挿入する。

　・Character1～６：セリフに合わせた表情
 
⑤SoundManagerにBGMを挿入する。（計５個）

　・StartBGM：タイトルシーンで流れるBGM
 
　・LifeBGM：ノベルシーンで流れるBGM
 
　・UnrestBGM：ノベルシーンで流れるBGM２
 
　・BattleBGM：パズルシーンで流れるBGM
 
　・FinalBGM：ノベルシーンで流れるBGM
 
⑥SoundManagerにSEを挿入する。（計１０個）

　・StartButton：スタートボタンを押した音
 
　・Button：ボタンを押した音
 
　・Block：ブロックに触れた音
 
　・Sword：剣の攻撃音
 
　・Ladle：お玉の攻撃音
 
　・Heart：ハートの回復音
 
　・Skull：毒のダメージ音
 
　・LastAttack：最後の攻撃音
 
　・Victory：勝利音
 
　・Creature：クリーチャーの鳴き声

### ◎Game_Over1、Game_Over2
①BG_Operationに背景画像等を挿入する。

　・Background：背景画像
 
　・Gallery1：イベントCG（外形画像の暗闇Var画像や薄黒い画像）
 
②Item_Operationにアイテム画像を挿入する。

　・Item1：魔法陣の画像
 
　・Item2：クリーチャーの画像
 
③Character_Operationにキャラクター画像を挿入する。

　・Character1～６：セリフに合わせた表情
 
④SoundManagerにBGMを挿入する。（計５個）

　・StartBGM：タイトルシーンで流れるBGM
 
　・LifeBGM：ノベルシーンで流れるBGM
 
　・UnrestBGM：ノベルシーンで流れるBGM２
 
　・BattleBGM：パズルシーンで流れるBGM
 
　・FinalBGM：ノベルシーンで流れるBGM
 
⑤SoundManagerにSEを挿入する。（計１０個）

　・StartButton：スタートボタンを押した音
 
　・Button：ボタンを押した音
 
　・Block：ブロックに触れた音
 
　・Sword：剣の攻撃音
 
　・Ladle：お玉の攻撃音
 
　・Heart：ハートの回復音
 
　・Skull：毒のダメージ音
 
　・LastAttack：最後の攻撃音
 
　・Victory：勝利音
 
　・Creature：クリーチャーの鳴き声

### ◎Middle_Scene
①Rule_Boxにルール画面の背景画像を挿入する。

②Ready_Ruttonにボタン画像を挿入する。

③BG_Operationに背景画像等を挿入する。

　・Background：背景画像
 
　・Gallery1：イベントCG（外形画像の暗闇Var画像や薄黒い画像）
 
④Item_Operationにアイテム画像を挿入する。

　・Item1：魔法陣の画像
 
　・Item2：クリーチャーの画像
 
⑤Character_Operationにキャラクター画像を挿入する。

　・Character1～６：セリフに合わせた表情
 
⑥SoundManagerにBGMを挿入する。（計５個）

　・StartBGM：タイトルシーンで流れるBGM
 
　・LifeBGM：ノベルシーンで流れるBGM
 
　・UnrestBGM：ノベルシーンで流れるBGM２
 
　・BattleBGM：パズルシーンで流れるBGM
 
　・FinalBGM：ノベルシーンで流れるBGM
 
⑦SoundManagerにSEを挿入する。（計１０個）

　・StartButton：スタートボタンを押した音
 
　・Button：ボタンを押した音
 
　・Block：ブロックに触れた音
 
　・Sword：剣の攻撃音
 
　・Ladle：お玉の攻撃音
 
　・Heart：ハートの回復音
 
　・Skull：毒のダメージ音
 
　・LastAttack：最後の攻撃音
 
　・Victory：勝利音
 
　・Creature：クリーチャーの鳴き声

### ◎Pazzle_Scene
①Backgroundにパズルゲーム画面の背景画像を挿入する。

②OffensivePowerFrameにブロックの効果説明画面の背景画像を挿入する。

③Board_Frameにパズル盤面の枠組み画像を挿入する。

④SoundManagerにSEを挿入する。（計１０個）

　・StartButton：スタートボタンを押した音
 
　・Button：ボタンを押した音
 
　・Block：ブロックに触れた音
 
　・Sword：剣の攻撃音
 
　・Ladle：お玉の攻撃音
 
　・Heart：ハートの回復音
 
　・Skull：毒のダメージ音
 
　・LastAttack：最後の攻撃音
 
　・Victory：勝利音
 
　・Creature：クリーチャーの鳴き声

### ◎Final_Scene
①CharacterのImageにキャラクター画像を挿入する。

②BG_Operationに背景画像等を挿入する。

　・Background：背景画像
 
　・Gallery1：イベントCG（外形画像の暗闇Var画像や薄黒い画像）
 
　・Gallery2：ラストシーンの背景画像
 
③Item_Operationにアイテム画像を挿入する。

　・Item1：クリーチャーの画像
 
　・Item2：扉の画像
 
④Character_Operationにキャラクター画像を挿入する。

　・Character1～６：セリフに合わせた表情
 
⑤SoundManagerにBGMを挿入する。（計５個）

　・StartBGM：タイトルシーンで流れるBGM
 
　・LifeBGM：ノベルシーンで流れるBGM
 
　・UnrestBGM：ノベルシーンで流れるBGM２
 
　・BattleBGM：パズルシーンで流れるBGM
 
　・FinalBGM：ノベルシーンで流れるBGM
 
⑥SoundManagerにSEを挿入する。（計１０個）

　・StartButton：スタートボタンを押した音
 
　・Button：ボタンを押した音
 
　・Block：ブロックに触れた音
 
　・Sword：剣の攻撃音
 
　・Ladle：お玉の攻撃音
 
　・Heart：ハートの回復音
 
　・Skull：毒のダメージ音
 
　・LastAttack：最後の攻撃音
 
　・Victory：勝利音
 
　・Creature：クリーチャーの鳴き声

### ◎その他
①DOTweenのインストール

　Change_SceneスクリプトやPazzleGameManagerスクリプトにおいて、DOTweenを使用して
 
　いるため、プロジェクトを実行する際は、DOTweenをダウンロードしてください。
 
②日本語対応のフォントインストール

　シナリオなどに使用するため、日本語文字を使用できるフォントをダウンロードしてください。
