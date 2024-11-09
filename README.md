# MVC-SQL
## 課程期末專題
原使用SQL Server做為資料連接的來源，並且為了讓組員可以連上我的本機上，在連線規則設定了一番，意外研究到得知老師是如何開啟資料庫連結，同時設定學生只可新增修改自己的table但不可刪除任何table的功能(可惜最後還是無法讓組員連上)
### 修改部分
將專案打掉重練，把SQL Server改為較簡易設定的SQLite。
其中CRUD全依靠MVC，且不須在龐大且多功能的圖形介面耗費時間加載、研究，連線也不須在appsettings.json使用"ConnectionStrings":{}(既是優點(連線管理)也是缺點(連線預處理))
### 已優化
+ Home index頁面跳轉
+ 登入、註冊流程及邏輯優化(可交互使用)
+ Status使用者名稱正常且穩定顯示
+ Navbar中Session使用修復完成
## 主要特色
### 基本的CRUD
可以註冊帳號(Create)、查看書籍資訊(Read)、修改(Update)或刪除(Delete)書籍
### 多類型用戶
限制不同類型的用戶可見的資料不同(已規劃但尚未實裝)
## 預計新增
+ 嘗試新增member
+ 陸續將原本資料恢復