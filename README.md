# Unity_Living_Lonely
유니티 3D 로 제작한 오픈월드 생존 게임
유니티 2021.3.0F1 버전
HDRP 파이프 렌더링 사용
Graphic api Vulkan
DB Playfab 사용

플레이 영상











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

9. 게임 요구사항 기술서 및 모듈설명
9.1 요구사항 기술서
9.2 모듈설명 및 코드 설명











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

인게임 화면에서는 wasd 로 플레이어가 움직일 수 있으며, c키로 앉기 shift키로 달리기등 플레이가 가능하며 가운데 상단에 일자 나침반 하단 좌측에 현재 착용중인 무기 아이콘,
하단 중앙에는 플레이어의 체력,스테미나,배고픔,목마름 상태 우측에는 탄약표시다. 
우측 상단에는 현재 생존한 시간이 표시된다. 화면중앙에는 크로스헤어로 크로스헤어 중앙으로 적을 맞추면 히트가된다.

i키를 누르면 인벤토리가 뜨는데 인벤토리 중앙에 슬롯에는 아이템들을 획득하면 칸에 맞게 늘어나고 하단에는 퀵슬롯으로 무기나 아이템을 등록하면 1번부터 5번까지 숫자를 눌러 빠르게 사용할 수 있다.
인벤토리는 i키 혹은 esc키를 눌러 닫을 수 있다.








Esc 키를 누를시에 일시정지 화면이 뜬다. 일시정지 화면에서는 튜토리얼 확인과, 메인화면으로 갈 수 있는 버튼이 있다. Esc를 다시누르면 일시정지가 풀린다.
3.게임의 배경 스토리
3.1 적 오브젝트 소개
적 오브젝트 들은 총 7가지로 좀비 6종류와 게임 시작 10분이 지나면 특별 스폰되는 기괴한 몬스터 한 오브젝트가 있다.
3.2 시나리오
바이러스와 이상현상으로 사람이 살 수 없게 된 가상의 섬 ‘체인버’에 특수 부대원인 ‘Adela(아델라)’를 파견하여 이상현상 바이러스의 원인을 파악하고 그들을 모두 제거하는 시나리오.
4. 그래픽 
4.1 게임의 그래픽 디자인 설명


HDRP 파이프렌더링으로 기본적인 그래픽을 설정하였으며 품질은 중간으로 하였다.
울트라로 하면 프레임 드랍이 엄청나게 생기는데 (쉐이더 문제로) 프레임이 절반정도 줄어든다. 품질은 떨어지지만 안정적인 프레임을 위해 전체적인 품질을 중간으로 설정
원래 다이렉트 12를 사용하여 레이트레이싱을 적용할려 했으나 프로젝트 오류로인해 API를 불칸으로 사용하였다.

렌더링 모드는 DLSS를 사용하였다. DLSS 를 사용하면 안티엘리어싱이 TAA밖에 사용하지 못하여 계단 현상이 있지만, 프레임 관리에 최적화되어 있어 DLSS를 사용하였다.
4.2 캐릭터 디자인 및 애니메이션 설명

플레이어의 총기 애니메이션이다.
1인칭 시점이기에 딱히 플레이어가 움직이는 것은 없고 총기 자체에 플레이어 손형태에 모델링이 같이 붙어있어서 총기, 근접무기 애니메이션으로 걷는 효과 뛰는 효과가 가능하다. 총기류 애니메이션은 오버라이드를 통해 모두 같은 애니메이션이다.
애니메이션에는 걷기, 뛰기 , 쏘기, 정조준 , 무기 꺼내기, 무기 넣기, 재장전. 정조준 사격 이있다.

근접무기 애니메이션이다. 오버라이드를 통해 모두 같은 애니메이션이다.
근접무기 애니메이션으로는 걷기, 뛰기, 공격 무기 꺼내기가 있다.

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

기본적으로 필드레벨에는 나무, 풀, 물로 구성되어 있고 그 위에 건물들이 있다. 그리고 건물안에는 파밍할 수 있는 책상이 존재한다. 레벨 구성은 간단한 편이다. 
6.2 난이도 조절 
난이도는 기본적으로 생성되어 있다. 테스트는 나 포함 3명이 진행하였는데, 나와 다른 한명은 5분을 넘기지 못했고 나머지 한명은 10분넘게 생존하는 모습을 보였다. 기본적으로 현재로써 난이도 조절은 각 저녁때 난이도 상승, 시작하고 각각 3분, 6분, 10분마다 좀비의 이동속도 및 공격력 그리고 새로운 적의 추가로 난이도가 점점 상승한다.
웬만하면 10분을 못넘기도록 루즈해지기 전에 끝내도록 난이도를 설계하였다.
7. 인터페이스 및 사용자 경험
7.1 메뉴 및 UI 디자인 설명
몇몇 설명은 2.2 절에서 설명하여 설명한 것은 간단히 진행.

일시정지UI
인게임 UI


인벤토리 UI

튜토리얼 UI로 총 5페이지가 있고 페이지마다 메인화면으로 갈 수 있으며 2페이지에서 4페이지까지는 이전페이지 및 다음페이지로가는 것이 존재하고 1페이지에는 다음페이지 버튼만, 5페이지는 이전페이지 버튼만 있다.



로그인 UI다 아이디와 비밀번호를 입력할 수 있고 아이디가 있다면 로그인 없다면 회원가입을 할 수 있다.

로그인을 성공하고 나오는 UI이다. 새로운 게임을 시작하거나 기존의 게임을 불러오거나 랭킹을 확인 할 수 있다.

랭킹을 표시하는 UI이다. 킬수와 생존시간이 오름차순으로 정렬되어 최대 10개까지 표시된다.

각각 플레이어마다 Playfab 디비에 세이브 파일 데이터 및 랭킹기록에 필요한 데이터 값이 적용된다. DB에서 값을 불러와서 세이브 파일 로드 및 랭킹 정보를 불러온다.
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

