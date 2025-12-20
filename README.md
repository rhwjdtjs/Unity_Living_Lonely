# Living Lonely (Unity 3D Survival Game)
> HDRP 기반 1인칭 유니티 생존 게임 / **PlayFab 로그인/클라우드 세이브/리더보드**

**About**
- 🏆 2023년 캡스톤 디자인-산학 캡스톤 디자인 연계 개인 프로젝트 (Unity)  

**Quick Links**
- 🎮 Gameplay Video: https://youtu.be/qtte7avW9yM?si=CLguTa88gA9RqQct  
- 📘 [Technical Doc](#0-TOC) 

---

## Introduction
**Living Lonely**는 배고픔/목마름/스태미나/HP를 관리하며, 무기(총기/근접)를 활용해 몬스터 웨이브를 버티는 **서바이벌 게임**입니다.  
저장/로드, 인벤토리(드래그&드롭), 퀵슬롯, 랜덤 아이템 스폰, 몬스터 스폰/난이도 스케일링, 그리고 PlayFab 기반 리더보드까지 “게임 한 판”이 성립하는 핵심 루프를 구현했습니다.

---

## Development
- **기간**: 2023.03 ~ 2023.06 
- **인원**: 1인 개발
- **환경**: Unity 2021.3.0f1 / HDRP / Vulkan / Windows 11 / VS 2022
- **역할**
  - Gameplay(C#): 플레이어 조작/전투/AI/상호작용/상태(HP·배고픔·목마름·스태미나)
  - UI·UX: 인벤/퀵슬롯/툴팁/일시정지/튜토리얼/로딩
  - Online Services: PlayFab 로그인·클라우드 세이브·랭킹(리더보드)

---

## Overview
- **탐색/루팅**: 랜덤 스폰 아이템을 획득해 장비를 갖추고 생존 리소스를 관리
- **전투/생존**: 총기·근접무기로 몬스터를 처치하며 생존 시간과 킬 카운트를 기록
- **진행/기록**: 저장/로드 + PlayFab에 기록 업로드(생존시간/킬) 및 Top10 리더보드 노출

---

## Highlights
- **Cloud Save & Load (PlayFab + Local JSON)**
  - 메인에서 PlayFab UserData를 받아 로컬 JSON으로 저장 후 게임씬 로드/복원
- **Survival Status System**
  - 배고픔/목마름/스태미나/HP가 시간 흐름에 따라 변화, 임계치 경고 및 도트 데미지
- **Inventory UX (Drag & Drop / Split Drop / Tooltip)**
  - 슬롯 교환, 퀵슬롯 연동, 외부 드롭 시 수량 입력 UI 호출, 툴팁 안내
- **Weapon Framework (Gun + Melee)**
  - 총기/근접 무기 구조 분리 + 무기 교체 코루틴 + 탄약을 인벤 슬롯에서 소모/장전
- **Spawner & Difficulty Events**
  - 몬스터 “유지 개체수” 스폰 + 특정 생존 시간 이후 강한 몬스터 웨이브 발생
- **Leaderboard**
  - 킬/생존시간 Top10 조회 + 기존 기록보다 높을 때만 업로드

---

## Key Features
- **플레이어 조작**
  - 이동/시점/달리기(스태미나)/앉기 + 인벤 오픈 시 커서/시점 제어
- **상호작용(E키)**
  - 레이캐스트 기반 줍기/상호작용, 아이템/무기 획득 루프
- **인벤토리/퀵슬롯**
  - 드래그&드롭, 퀵슬롯(1~6)로 장착/소비 아이템 사용
- **총기 전투**
  - 파이어레이트/반동/정조준(사이트) + 히트 레이캐스트 기반 데미지 적용  
  - 무기별 탄약 타입(예: 5.56mm, 7.62mm 등) 인벤 슬롯에서 가져와 장전
- **근접 전투**
  - 공격 애니메이션 타이밍(스윙 윈도우) + 레이캐스트로 타격 판정
- **AI**
  - NavMeshAgent 기반 랜덤 워킹/추격/공격, 밤/생존시간에 따른 스피드 스케일링
- **게임 플로우**
  - 비동기 로딩(프로그레스바) → 게임 진행 → 사망/리셋/재시작
- **일시정지/튜토리얼**
  - ESC 일시정지(TimeScale) + 튜토리얼 패널 네비게이션
- **저장/로드**
  - 플레이어 위치/회전, 인벤 슬롯, 무기 탄수 등 상태를 저장/복원
- **랭킹**
  - PlayFab 통계 업데이트 + Top10 리더보드 UI 출력

---

## Tech Stack
![Unity](https://img.shields.io/badge/Unity-000000?style=flat&logo=unity&logoColor=white)
![C%23](https://img.shields.io/badge/C%23-512BD4?style=flat&logo=csharp&logoColor=white)
![HDRP](https://img.shields.io/badge/HDRP-444444?style=flat)
![Vulkan](https://img.shields.io/badge/Vulkan-AC162C?style=flat&logo=vulkan&logoColor=white)
![PlayFab](https://img.shields.io/badge/PlayFab-107C10?style=flat&logo=xbox&logoColor=white)
![Photon](https://img.shields.io/badge/Photon-00B8FF?style=flat)

- **Engine**: Unity 2021.3.0f1 (HDRP / Vulkan)
- **Language**: C#
- **Online Services**: PlayFab (Login / Cloud Save / Leaderboard)

---

## Media
### Gameplay (Single Player)
[![Living Lonely DEMO](https://github.com/user-attachments/assets/72957aea-12c4-4d67-853a-48e4cd169764)](https://youtu.be/qtte7avW9yM?si=laPsIAj5Zh6W86HJ)

---


<details>
  <summary><b>📘 Technical Documentation (Living Lonely 기술서) (펼치기)</b></summary>

<a id="0-TOC"></a>
## TOC
- [1. 전체 게임 루프](#1-전체-게임-루프)
- [2. 로딩 & 씬 전환](#2-로딩--씬-전환)
- [3. 저장/로드 설계](#3-저장로드-설계)
- [4. 플레이어 컨트롤 & 생존 스탯](#4-플레이어-컨트롤--생존-스탯)
- [5. 인벤토리 UX](#5-인벤토리-ux)
- [6. 무기 시스템](#6-무기-시스템)
- [7. 몬스터 AI & 스폰/난이도](#7-몬스터-ai--스폰난이도)
- [8. 랭킹 시스템](#8-랭킹-시스템)
- [9. 일시정지/튜토리얼](#9-일시정지튜토리얼)
- [10. 조작키](#10-조작키)

---

## 1. 전체 게임 루프
**Start → Load(선택) → Spawn → Explore/Loot → Combat → Record → Save/Upload → Dead/Restart**

- 플레이어 스폰 포인트 랜덤 선택 후 시작
- 생존 시간/킬을 기록하고, 조건에 따라 랭킹 업데이트
- 사망 시 리셋/재시작 루프 제공

---

## 2. 로딩 & 씬 전환
- LoadingScene에서 AsyncOperation 기반 비동기 로드 + 진행률 UI 갱신
- 메인에서 “저장된 데이터 수신 → 로컬 저장 → 게임씬 로드” 플로우 구성

---

## 3. 저장/로드 설계
- 저장 데이터:
  - 플레이어 Transform(위치/회전)
  - 인벤 슬롯(인덱스/아이템명/개수)
  - 무기별 현재 장전 탄수
- 저장 매체:
  - 로컬 JSON(즉시 복원 가능)
  - PlayFab UserData(클라우드 백업/동기화)

---

## 4. 플레이어 컨트롤 & 생존 스탯
- 이동/시점/달리기(스태미나 소모)/앉기
- 배고픔/목마름 감소 → 0 도달 시 주기적 HP 감소(도트)
- 사망 시 기록 처리 및 UI/재시작 플로우

---

## 5. 인벤토리 UX
- 슬롯 드래그&드롭 교환
- 인벤 외부 드롭 시 “수량 입력 UI” 호출(분할 드랍)
- 툴팁: 아이템 타입에 따라 RMB 액션(Equip/Eat) 가이드

---

## 6. 무기 시스템
### 6.1 공통 WeaponManager
- 무기 교체 코루틴(딜레이/애니메이션 트리거)
- 교체 시 재장전/정조준 등 행동 Cancel 후 타입별 활성화 플래그 관리

### 6.2 총기(GunMainController + 무기별 컨트롤러)
- 발사/정조준/반동/재장전 코루틴
- 탄약은 인벤 슬롯에서 타입별로 집계 → Reload 시 슬롯에서 차감
- 현재 장전 탄수는 저장 데이터에 연동하여 Load 시 복원 가능

### 6.3 근접(MeleeWeaponControllor)
- 공격 애니메이션 타이밍(스윙 윈도우) 기반으로 Raycast 타격 판정
- 손/도끼/나이프 등 장비 타입별 교체 지원

---

## 7. 몬스터 AI & 스폰/난이도
- NavMeshAgent 기반 이동(랜덤 워킹/추격/공격)
- 생존시간/상태(밤 등)에 따른 이동속도 스케일링
- 스폰 매니저:
  - “유지 몬스터 수” 유지 스폰
  - 특정 생존시간 이상 시 강한 몬스터 웨이브 이벤트

---

## 8. 랭킹 시스템
- PlayFab Statistics:
  - KillCount / SurvivalTimeSeconds 업로드
  - 기존 기록보다 높을 때만 업로드(불필요한 호출 방지)
- 리더보드 UI:
  - Top10 조회 후 UI 텍스트 갱신

---

## 9. 일시정지/튜토리얼
- ESC 일시정지(Time.timeScale), 커서 락/해제, 메인 메뉴 이동
- 튜토리얼 패널: Next/Prev로 페이지 네비게이션

---

## 10. 조작키
- 이동: WASD
- 달리기: Shift
- 앉기: C
- 인벤: I
- 상호작용/줍기: E (프로젝트 설정에 따라)
- 일시정지: ESC
- 저장/업로드: F5 (구현 기준)
- 메인 메뉴: F6 (구현 기준)

</details>

## Detail Gallery (Screenshots)

<details>
  <summary><b>🖼️ Detail Gallery 펼치기</b></summary>

<br/>

### Gameplay / UI

<a id="gallery-video"></a>
**Gameplay Video Thumbnail**
![Gameplay](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/ea85cd52-bfcd-45d6-b789-bc1d11a6c0db)

<a id="gallery-system"></a>
**System Overview / UI Explanation**
![System](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/9d1d8755-0a7c-4133-be68-e7b74a329d77)

<a id="gallery-mainmenu"></a>
**Main Menu**
![MainMenu](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/41e06cba-1222-4710-a821-53268a8d4f2f)

<a id="gallery-ingamehud"></a>
**In-Game HUD**
![InGameHUD](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/182d465d-98ad-45ad-93f5-19a0db331531)

<a id="gallery-inventory"></a>
**Inventory**
![Inventory](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/5d7c3f30-92ef-4098-b7c3-db9c8dfb8e35)

---

### Enemies / World / Graphics

<a id="gallery-enemy"></a>
**Enemy Objects**
![Enemy](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/5c2b695b-940e-4fc8-800e-7db3d1caa398)

<a id="gallery-graphics-1"></a>
**Graphics (HDRP / Scene)**
![Graphics1](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/e1016fcb-6866-4362-ab37-4a574b46aea5)
![Graphics2](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/c050062b-9e15-4671-bc8b-8fb5e123d643)

<a id="gallery-rendering"></a>
**Rendering / Settings**
![Rendering](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/87218a42-814f-4fd2-ac4e-7bf5ffda68dd)

<a id="gallery-anim"></a>
**Animations (Gun / Melee / Enemies)**
![GunAnim](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/6ae5cda0-db77-486f-b88d-6f1287611aca)
![MeleeAnim](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/9f3dd8d9-aefb-4384-9a41-3043354c8f19)
![EnemyAnim](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/04499ddb-2eae-4e5f-8dfb-6826d61eee52)

<a id="gallery-level"></a>
**Level Design**
![Level1](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/16a4b470-94e9-48ca-ac2e-8c2c6107023d)
![Level2](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/bfc87091-a24a-4afc-99d7-5947c90dacab)
![Level3](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/e3e107e3-9ffa-48ef-b068-d06bd4466c05)

---

### Menus / UX Screens

<a id="gallery-ux"></a>
**Pause / InGame / Inventory UI**
![PauseUI](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/d4ca66be-1a7f-492d-88e9-03cc4d3e0db2)
![InGameUI](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/cc044f64-4799-486b-af3d-28e88b931e13)
![InventoryUI](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/69a2aabe-d2e2-479a-b1b3-66710390aadd)
![TutorialUI](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/f1ad506f-109c-484c-ab0a-0c74337bce26)

<a id="gallery-login"></a>
**Login / Lobby / Ranking**
![Login1](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/911fd196-7a0f-4a3b-8e05-0fc419acf7d4)
![Login2](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/585611b1-f43b-483b-915b-36e714655c37)
![AfterLogin](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/06abd46a-2e68-43cc-bafc-6771f65ed8de)
![Ranking1](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/c55b8657-3f78-4708-93cc-858b2ed719e5)
![Ranking2](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/8f60016b-ca5e-426c-86f4-b18056f0188a)

---

### Docs / Modules

<a id="gallery-req"></a>
**System Requirements Doc**
![Req](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/6489ff91-d54e-453c-b6fb-8733f54dc5d6)

<a id="gallery-modules"></a>
**Module Overview**
![ModuleOverview](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/6133bab3-7dcf-45cc-beb8-4878c89fef4e)

<a id="gallery-module-item"></a>
**Item Module**
![ItemModule](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/ca6e6781-145f-4725-8d4e-06a8f4799319)

<a id="gallery-module-display"></a>
**Display Module**
![DisplayModule](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/6eb59d7c-1f6d-4828-a506-48d363f2af70)

<a id="gallery-module-object"></a>
**Object Module**
![ObjectModule](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/028a14a4-929b-4f1c-acb5-89286697993d)

<a id="gallery-module-env"></a>
**Environment Module**
![EnvModule](https://github.com/rhwjdtjs/Unity_Living_Lonely/assets/42109688/4276bd21-b92e-40ce-96cc-84e91c3801ea)

<br/>
</details>

