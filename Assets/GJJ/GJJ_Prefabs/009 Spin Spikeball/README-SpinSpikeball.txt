< GJJ_TrapSpinSpikeball 메뉴얼 >

특정 지점을 중심으로, 다수의 가시 공이 생성되어 원을 그리며 회전하는 트랩입니다.

1. GJJ_TrapSpinSpikeball
    - 하위 객체로 Anchor 를 갖고 있으며, Anchor 은 하나만 존재해야 한다.
    - 목록
        - Spike Ball : 생성할 가시 공의 Prefab 을 설정한다.
        - Anchor : 가시 공이 생성될 기준점을 설정한다.
        - Spin Time : 가시 공이 회전할 속도를 설정한다.
        - Spike Minimum Arm Length : 가시 공 사이의 간격을 설정한다.
        - Spike Ball Count : 생성할 가시 공의 개수를 설정한다.

2. GJJ_TrapSpinSpikeball_Ballhead
    - DamageCollider 스크립트를 이용하고 있다.
        - 이 함정이 주는 피해는 이 곳에서 설정한다.
    - 충돌 판정으로 Polygon Collider 2D 를 사용하고 있다.