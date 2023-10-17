< GJJ_TileSuperJump 메뉴얼 >

순간적으로 높게 뛰어오를 수 있는 점프대이다.

Prefab 내 구성물
1. GJJ_TileSuperJump
    - 점프대의 외형이다.
    - 플레이어 캐릭터가 점프대를 밀 수 있도록, Rigidbody2D 와 Box Collider 2D 가 붙어있다.

2. JumpCheckpoint
    - 점프대의 기능부이다.
    - Box Collider 2D
        - 플레이어가 닿으면 튀어오를 수 있도록 설정한다.
        - Platform Effector 2D 와 사용하기 위해 Used by Effetor 를 체크한다.
    - Platform Effector 2D
        - 플레이어가 점프대의 밑에서 올라와, 점프대를 밟고 튀어오를 수 있도록 한다.
        - Collider Mask 를 설정해, 플레이어 캐릭터만 뛰어오를 수 있도록 혹은 다른 설정을 할 수 있다.
        - Surface Arc 를 90 으로 설정해, 점프대의 옆에 닿았다고 위로 튀어오르는 현상을 줄였다.