< 타일의 속성 설정하기 >

1. 필요한 스크립트를 확인합니다.
    - 위치 : Assets/GJJ/GJJ_Scripts
    - 스크립트
        - GJJ_TileCondition
        - GJJ_TileFalling
        - GJJ_TileMoving

2. 원하는 타일을 생성합니다.
    - Hierarchy 에서 2D Object - Tilemap 순서로 클릭해 새 Tilemap 을 생성합니다.
    - Tilemap Palatte 를 이용해 원하는 모양으로 타일을 만듭니다.
    - Tilemap 을 Prefab 으로 만들어둡니다.
        - Grid 는 Prefab 에 포함되지 않습니다.

3. 필요한 컴포넌트를 붙이고 설정합니다.
    - (1) 의 스크립트를 전부 붙여줍니다.
    - RigidBody2D, Platform Effector 2D, 타일에 맞는 Collider 2D 를 붙입니다.
        - Collider 2D 의 'Used by Effector' 를 체크합니다.
        - Platform Effector 2D 의 Collider Mask 를 Player 로 설정합니다.
        - Platform Effector 2D 의 Surface Arc 를 175 로 설정합니다.
        - RigidBody2D 의 Body Type 을 Kinematic 으로 설정합니다.
        - RigidBody2D 의 Material 에 필요한 머티리얼을 연결합니다.