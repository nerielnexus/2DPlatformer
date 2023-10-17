< GJJ_SlowdownBush 메뉴얼 >

풀숲 혹은 특정 영역에 플레이어 캐릭터가 들어오면 이동속도, 점프력 등이 감소한다.

1. 컴포넌트
    - Polygon Collider 2D
        - (기본)풀숲모양에 맞는 Collider 영역을 설정한다.
        - 다른 이미지를 사용해 다른 모양이 된다면 맞는 Collider 를 사용한ㄷ아.
        - Area Effector 2D 와 함께 사용하기 위해 Used by Effector 를 체크한다.
        - 플레이어와 물리적 충돌을 하지 않으니 Is Trigger 를 체크한다.
    - Area Effector 2D
        - 플레이어 캐릭터와만 충돌을 체크하기 위해 Collider Mask 를 Player 로 설정한다.

2. GJJ_TrapSlowdownBushes
    - Bush Debuff
        - 플레이어가 풀숲 영역에 들어오면 실제로 받는 디버프 양을 설정한다.
            - Speed Walk (걷기 속도)
            - Speed Sprint (달리기 속도)
            - Speed Limit (최대 속도)
            - Jump Power (점프력)
            - Jump Count (점프 횟수)
            - Jump Max Count (최대 점프 횟수)
        - Player 클래스에서 값을 받아와 수정하는 방식으로 변경하고 있다.