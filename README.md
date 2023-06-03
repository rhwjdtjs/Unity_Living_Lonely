# Unity_Living_Lonely
유니티 3D 로 제작한 오픈월드 생존 게임  
유니티 2021.3.0F1 버전  
HDRP 파이프 렌더링 사용  
Graphic api Vulkan  
DB Playfab 사용  

플레이 영상
![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/ea85cd52-bfcd-45d6-b789-bc1d11a6c0db)
https://youtu.be/qtte7avW9yM 유튜브 링크


일부 코드는 인프런에서 수강한 케이디님의 강의에서 도움 받은 코드도 있습니다.  
무분별한 코드사용은 중지 부탁드립니다.  
현재는 관련코드를 내린 상태입니다.  
https://www.inflearn.com/course/unity-2/dashboard  
제작에 도움이 된 인프런 강의입니다.  
  
Some of the codes were helped by Kedy's lectures that I took on Infron.  
Please stop using the code indiscriminately.  
Currently, the relevant code has been issued.  
https://www.inflearn.com/course/unity-2/dashboard  
It's a lecture that helped with the production.  
  
목차  
1. 게임 개요  
1.1 게임 콘셉트  
1.2 목표 및 대상 플레이어  
1.3 게임 장르  
1.4 게임 플레이 설명  
  
2. 게임 시스템  
2.1 게임 규칙  
2.2 게임 시스템 설명   
  
3. 게임의 배경 스토리  
3.1 적 오브젝트 소개  
3.2 시나리오  
  
4. 그래픽   
4.1 게임의 그래픽 디자인 설명  
4.2 캐릭터 디자인 및 애니메이션 설명  
  
5. 사운드 및 음악  
5.1 배경 음악 및 효과음 설명  
5.2 캐릭터 및 NPC의 음성 설명  
  
  
6. 레벨디자인  
6.1 레벨 구성 요소 설명  
6.2 난이도 조절   
  
7. 인터페이스 및 사용자 경험  
7.1 메뉴 및 UI 디자인 설명  
  
8. 게임 실행을 위한 하드웨어 및 소프트웨어 요구사항  
8.1 시스템 요구사항 및 플랫폼 호환성 및 운영 체제 지원  

  
  
1. 게임 개요  
1.1 게임 콘셉트  
게임은 3D 오픈월드 생존게임으로 넓은 필드에서 끝없이 생성되는 좀비들로부터 생존하며 아이템을 파밍하고 최대한 오래 생존하는 컨셉, 전체적인 시스템은 스팀의 Dayz standalone와 유사하다.  
  
1.2 목표 및 대상 플레이어  
게임의 목표로는 현재 개발 현황으로써는 최대한 오래 생존하는 것이다. 오래 생존할수록 난이도가 점점 올라가 게임의 루즈함은 줄어들 것이다.  
대상 플레이어로는 빠른 진행의 게임을 좋아하는 플레이어에게 잘 맞는 게임이다.  
  
1.3 게임 장르  
게임 장르로는 서바이버 호러 Fps 장르이다.  
  
1.4 게임 플레이 설명  
플레이어는 게임을 시작하면 필드에 램덤으로 배치해 좀비들을 피해 다니거나 죽이면서 건물에 들어가 아이템을 파밍하고 시간이 지날수록 점점 강해지는 적과 싸우며 생존하는 것이 게임 플레이 방식이다.  

‘2. 게임시스템  

2.1 게임 규칙   
게임 규칙은 간단하다. 오픈월드인지라 개발된 기능적으로는 플레이어 행동에 따로 제한은 없다. 오래생존 및 적을 최대한 많이 죽여 자신의 기록을 랭킹에 기록하여 다른 플레이어와 경쟁하는 것이다.  
  
2.2 게임 시스템 설명  
![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/9d1d8755-0a7c-4133-be68-e7b74a329d77)  
  
메인화면이다.  
메인화면에서는 튜토리얼 확인, 게임 로그인하여 랭킹보기 및 게임 시작 그리고 종료가 있다.  
![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/41e06cba-1222-4710-a821-53268a8d4f2f)  
  
인게임 화면에서는 wasd 로 플레이어가 움직일 수 있으며, c키로 앉기 shift키로 달리기등 플레이가 가능하며 가운데 상단에 일자 나침반 하단 좌측에 현재 착용중인 무기 아이콘,  
하단 중앙에는 플레이어의 체력,스테미나,배고픔,목마름 상태 우측에는 탄약표시다.   
우측 상단에는 현재 생존한 시간이 표시된다. 화면중앙에는 크로스헤어로 크로스헤어 중앙으로 적을 맞추면 히트가된다.  
![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/182d465d-98ad-45ad-93f5-19a0db331531)  
  
i키를 누르면 인벤토리가 뜨는데 인벤토리 중앙에 슬롯에는 아이템들을 획득하면 칸에 맞게 늘어나고 하단에는 퀵슬롯으로 무기나 아이템을 등록하면 1번부터 5번까지 숫자를 눌러 빠르게 사용할 수 있다.  
인벤토리는 i키 혹은 esc키를 눌러 닫을 수 있다.  

![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/5d7c3f30-92ef-4098-b7c3-db9c8dfb8e35)  

Esc 키를 누를시에 일시정지 화면이 뜬다. 일시정지 화면에서는 튜토리얼 확인과, 메인화면으로 갈 수 있는 버튼이 있다. Esc를 다시누르면 일시정지가 풀린다.  
3.게임의 배경 스토리  
3.1 적 오브젝트 소개  
![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/5c2b695b-940e-4fc8-800e-7db3d1caa398)  

적 오브젝트 들은 총 7가지로 좀비 6종류와 게임 시작 10분이 지나면 특별 스폰되는 기괴한 몬스터 한 오브젝트가 있다.  
3.2 시나리오  
바이러스와 이상현상으로 사람이 살 수 없게 된 가상의 섬 ‘체인버’에 특수 부대원인 ‘Adela(아델라)’를 파견하여 이상현상 바이러스의 원인을 파악하고 그들을 모두 제거하는 시나리오.  
4. 그래픽   
4.1 게임의 그래픽 디자인 설명  
![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/e1016fcb-6866-4362-ab37-4a574b46aea5)  
![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/c050062b-9e15-4671-bc8b-8fb5e123d643)  
  
HDRP 파이프렌더링으로 기본적인 그래픽을 설정하였으며 품질은 중간으로 하였다.  
울트라로 하면 프레임 드랍이 엄청나게 생기는데 (쉐이더 문제로) 프레임이 절반정도 줄어든다. 품질은 떨어지지만 안정적인 프레임을 위해 전체적인 품질을 중간으로 설정  
원래 다이렉트 12를 사용하여 레이트레이싱을 적용할려 했으나 프로젝트 오류로인해 API를 불칸으로 사용하였다.  
![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/87218a42-814f-4fd2-ac4e-7bf5ffda68dd)  

렌더링 모드는 DLSS를 사용하였다. DLSS 를 사용하면 안티엘리어싱이 TAA밖에 사용하지 못하여 계단 현상이 있지만, 프레임 관리에 최적화되어 있어 DLSS를 사용하였다.  
4.2 캐릭터 디자인 및 애니메이션 설명  
![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/6ae5cda0-db77-486f-b88d-6f1287611aca)  

플레이어의 총기 애니메이션이다.  
1인칭 시점이기에 딱히 플레이어가 움직이는 것은 없고 총기 자체에 플레이어 손형태에 모델링이 같이 붙어있어서 총기, 근접무기 애니메이션으로 걷는 효과 뛰는 효과가 가능하다. 총기류 애니메이션은 오버라이드를 통해 모두 같은 애니메이션이다.  
애니메이션에는 걷기, 뛰기 , 쏘기, 정조준 , 무기 꺼내기, 무기 넣기, 재장전. 정조준 사격 이있다.  
![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/9f3dd8d9-aefb-4384-9a41-3043354c8f19)  

근접무기 애니메이션이다. 오버라이드를 통해 모두 같은 애니메이션이다.  
근접무기 애니메이션으로는 걷기, 뛰기, 공격 무기 꺼내기가 있다.  
![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/04499ddb-2eae-4e5f-8dfb-6826d61eee52)  

적 오브젝트 애니메이션이다. 모두 오버라이드로 형태는 같고 종류로는 맞았을 때, 걸을때, 소리칠때. 죽을때, 달릴때, 공격할때 애니메이션이 있다.  
5. 사운드 및 음악  
5.1 배경 음악 및 효과음 설명  
배경음악으로는 기본적인 자연의 소리를 담고 있다.  
해당 BGM은 메인화면 인게임에서 무한루프로 작동된다.  
효과음으로는 총기 재장전 사운드. 타격소리, 총기 사운드가 있다.  
5.2 캐릭터 및 NPC의 음성 설명  
메인캐릭터 사운드로는 걷기, 달리기의 발소리 스테미나가 다 소진瑛 때 헉헉되는 소리와, 피격시 신음 소리가 있다. 적 오브젝트의 음성으로는 기본적인 소리치는 사운드와 피격 사운드, 사망사운드가 있다.  

6. 레벨디자인  
6.1 레벨 구성 요소 설명  
![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/16a4b470-94e9-48ca-ac2e-8c2c6107023d)  
![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/bfc87091-a24a-4afc-99d7-5947c90dacab)  
![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/e3e107e3-9ffa-48ef-b068-d06bd4466c05)  

기본적으로 필드레벨에는 나무, 풀, 물로 구성되어 있고 그 위에 건물들이 있다. 그리고 건물안에는 파밍할 수 있는 책상이 존재한다. 레벨 구성은 간단한 편이다.   
6.2 난이도 조절   
난이도는 기본적으로 생성되어 있다. 테스트는 나 포함 3명이 진행하였는데, 나와 다른 한명은 5분을 넘기지 못했고 나머지 한명은 10분넘게 생존하는 모습을 보였다. 기본적으로 현재로써 난이도 조절은 각 저녁때 난이도 상승, 시작하고 각각 3분, 6분, 10분마다 좀비의 이동속도 및 공격력 그리고 새로운 적의 추가로 난이도가 점점 상승한다.  
웬만하면 10분을 못넘기도록 루즈해지기 전에 끝내도록 난이도를 설계하였다.  
7. 인터페이스 및 사용자 경험  
7.1 메뉴 및 UI 디자인 설명  
몇몇 설명은 2.2 절에서 설명하여 설명한 것은 간단히 진행.  
![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/d4ca66be-1a7f-492d-88e9-03cc4d3e0db2)  

일시정지UI  
![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/cc044f64-4799-486b-af3d-28e88b931e13)  

인게임 UI  

![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/69a2aabe-d2e2-479a-b1b3-66710390aadd)  

인벤토리 UI  
![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/f1ad506f-109c-484c-ab0a-0c74337bce26)  

튜토리얼 UI로 총 5페이지가 있고 페이지마다 메인화면으로 갈 수 있으며 2페이지에서 4페이지까지는 이전페이지 및 다음페이지로가는 것이 존재하고 1페이지에는 다음페이지 버튼만, 5페이지는 이전페이지 버튼만 있다.  


![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/911fd196-7a0f-4a3b-8e05-0fc419acf7d4)  

로그인 UI다 아이디와 비밀번호를 입력할 수 있고 아이디가 있다면 로그인 없다면 회원가입을 할 수 있다.  
![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/585611b1-f43b-483b-915b-36e714655c37)  

로그인을 성공하고 나오는 UI이다. 새로운 게임을 시작하거나 기존의 게임을 불러오거나 랭킹을 확인 할 수 있다.  
![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/06abd46a-2e68-43cc-bafc-6771f65ed8de)  

랭킹을 표시하는 UI이다. 킬수와 생존시간이 오름차순으로 정렬되어 최대 10개까지 표시된다.  

각각 플레이어마다 Playfab 디비에 세이브 파일 데이터 및 랭킹기록에 필요한 데이터 값이 적용된다. DB에서 값을 불러와서 세이브 파일 로드 및 랭킹 정보를 불러온다.  
![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/c55b8657-3f78-4708-93cc-858b2ed719e5)  
![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/8f60016b-ca5e-426c-86f4-b18056f0188a)  

8. 게임 실행을 위한 하드웨어 및 소프트웨어 요구사항  
8.1 시스템 요구사항 및 플랫폼 호환성 및 운영 체제 지원  
운영체제는 윈도우10, 윈도우11에서 진행을 하였고, 안드로이드 및 맥에서는 불가능하다.  
요구사항으로는 현재 따로 그래픽설정이 없는 가운데  
테스트해본 결과 최소사양으로는  
(테스트 기종으로 최소 / 최대 기준)  
최소사양                 원만한 플레이  
I5 9400F                 I7 11700F   
RAM 8GB                RAM 16GB  
RTX 2060                RTX2070SUPER  
최소 사양 기준으로 평균 50~60프레임 나온다.  

요구 사항 기술서  
![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/6489ff91-d54e-453c-b6fb-8733f54dc5d6)

게임 모듈  
![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/6133bab3-7dcf-45cc-beb8-4878c89fef4e)  
게임시스템모듈에 사용한 스크립트  
1.SaveNLoad.cs(게임의 저장 및 불러오기 기본적인 함수가 있는 스크립트)  
2.Maingameload.cs(게임의 데이터를 가지고 로드하는데 필요한 스크립트)  
3. 나머지 기능은 엔진내에서 기능구현함.  

![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/ca6e6781-145f-4725-8d4e-06a8f4799319)  
아이템모듈이다.  
1. EffectItem.cs(기본적인 아이템 효과를 받기위한 스크립트이다.)  
2. Item.cs(아이템의 정보를 기록하는 스크립트이다.)  
3. StatusController.cs(아이템의 효과를 받기위해 플레이어 상태를 나타내는 스크립트이다.)  
4. GunTextEditor.cs(현재 장탄수를 표시하게 해주는 스크립트이다.)  
5. CheckActions.cs(필드에 있는 아이템을 화면 중앙에 위치시키면 주울수 있는 ui가 뜨도록 하는 스크립트이다.)  
6. InputNumber.cs(인벤토리에 있는 아이템을 필드에 버릴 때 사용하는 스크립트이다.)  
7. inventory.cs(아이템을 얻고 인벤토리 ui를 기본적으로 구성하는 스크립트이다.)  
8. QuickSlot.cs(인벤토리 외 퀵슬롯을 구성하기 위한 스크립트이다.)  
9. Slot.cs(아이템을 획득하면 인벤토리 내에 슬롯에 각각 저장해야 하는데 그걸 위한 스크립트이다. 또한 아이템 드래그와 같은 기능을 담당한다.)  
10.SlotUtil.cs(기본적으로 슬롯의 ui를 담당한다.)  
11.ToolTip.cs(인벤토리의 아이템을 마우스로 가져다대면 아이템의 정보가 나오느데 그걸 위한 스크립트이다.)  
12.Gun.cs(총의 반동정도, 	재장전시간, 총의 이름과 같은 기본적인 정보를 담고 있는 스크립트이다.)  
13.GunmainController.cs(총기의 공통적인 부분인 정조준, 사격, 애니메이션과 같은 공통된 기능들이 담겨있는 스크립트이다.)  
14.PISTOL1~2controller, rifle1~3controller, tommyguncontroller.cs(각각 총기들의 스크립트로 gunmaincontroller의 자식객체들이다. 총마다 재장전에 따른 사운드 다른 탄약을 쓰게 때문에 각각의 스크립트를 갖고있다.)  
15. meleeweapon.cs(gun.cs와 마찬가지로 근접공격의 이름, 공격 딜레이와 같은 기본적인 정보가 담겨있는 스크립트이다.)  
16. meleeweaponcontroller.cs(객체 판단, 공격, 및 타격, 무기교체와 같은 기능이 담겨있는 스크립트이다.  
17.knifecontroller, axecontroller,handcontroller.cs(각각의 근접무기 스크립트로 meleeweaponcontroller의 자식객체로 각각의 무기 애니메이션, 히트 관련하여 스크립트가 있다.)  
18.weaponmanager.cs(무기를 교체하기위해 무기정보를 등록한 무기 관리 스크립트이다.)  

![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/6eb59d7c-1f6d-4828-a506-48d363f2af70)   
디스플레이 모듈이다. 대부분 유니티 엔진내에서 기능을 담당하지만  
스크립트도 포함되어 있다.  
1.	flashlightcontroller.cs(플레시라이트 아이템이 있을시 불을 끄고 키게하는 기능을 가진 스크립트이다.)  
2.	statuscontroller.cs(플레이어의 목마름,배고픔,체력,기력을 나타내는 스크립트이다.)  

![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/028a14a4-929b-4f1c-acb5-89286697993d)  
오브젝트 모듈은 사운드 적인 부분은 엔진내에서 처리 나머지는 스크립트로 구동된다.  
1.	Monster.cs(몬스터들의 이름이나 걷기속도와 같은 기본적인 기능을 가지고 있는 스크립트이다.)  
2.	Attackmonster.cs(몬스터들중에 공격하는 몬스터들의 기능을 가지고 있는 스크립트이다.)  
3.	Creature.cs(공격하는 몬스터들중에 이름이 creature인 몬스터들의 기능을 담고 있는 스크립트이다. 현재는 좀비와 괴물만 있지만 둘이 같은 스크립트를 사용한다.)  
4.	FieldofView.cs(좀비의 기본적인 ai를 담당한다. 좀비가 플레이어를 인지 및 공격을 하는데 주로 사용되는 스크립트이다.)  
5.	Playercontroller.cs(플레이어의 기본적인 움직임을 담당하는 스크립트이다. 위에서 사용되는건 발소리 관련부분이 해당 스크립트에 있다.  
6.	ItemRandomSpawn.cs(필드에 램덤으로 아이템을 생성하는 스크립트이다.)  
7.	Playerspawnmanager.cs(플레이어를 지정된 장소에서 랜덤으로 배치하는 스크립트이다.)  
8.	Spawnmanager.cs(좀비의 스폰을 담당하는 스크립트이다.  
다음으로는 환경모듈이다.  
환경모듈도 대부분의 기능이 유니티엔진내에서 작동한다.  
사용된 스크립트는  
1.	DayController.cs(태양을 일정속도로 회전시켜 낮과 밤을 구분한다.)  

![image](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/4276bd21-b92e-40ce-96cc-84e91c3801ea)  

마지막으로 통신모듈이다.  
통신모듈은 모두 스크립트를 이용하였다.  
1.	RankingUI.CS(랭킹의 UI를 담당하는 스크립트이다.)  
2.	RealtimeRankingSystem.cs(플레이어의 킬수와, 생존시간을 업로드하고 업로드한 데이터를 받아와 랭킹 ui에 표시하게 해주는 스크립트이다.)  
3.	MaingameLoad.cs(플레이 팹에서 업로드된 플레이어 세이브 데이터를 가져와 게임을 이어서하게 해준다.)  
4.	Playfablogin.cs(플레이팹에 로그인하게 해주는 스크립트이다.  
5.	Playfabsave.cs(플레이 팹 디비에 게임 데이터를 업로드하게 해주는 스크립트이다.  





