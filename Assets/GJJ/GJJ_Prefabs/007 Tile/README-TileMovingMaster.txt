< 움직이는 타일을 씬에 추가하기 >

1. GJJ_TileMovingMaster 를 Hierarchy 에 추가합니다.

2. GJJ_TileMovingMaster 에 빈 오브젝트를 2개 이상 추가합니다.
    - 이 빈 오브젝트들은 Tile 이 따라 움직일 Waypoint 역할을 합니다.

3. GJJ_TileMovingMaster 의 Inspector 에서 세부 사항을 설정합니다.
    - Tile
        - 생성해서 움직이게 할 타일 Prefab 입니다.
        - Tilemap 오브젝트를 생성해서, 원하는 Tileset 으로 타일을 만들어 등록할 수 있습니다.
    - Tile Rally Type
        - 타일이 어떻게 움직일지 설정합니다.
        - NONE 은 기본값이며, 타일이 움직이지 않습니다.
        - STRAIGHT 는 Waypoint 를 순서대로 한 번씩 지나가며, 마지막 Waypoint 에서 타일이 Destroy 됩니다.
        - PINGPONG 은 첫 Waypoint 와 끝 Waypoint 를 왕복하며 이동합니다.
        - CIRCLE 은 STRAIGHT 와 같이 첫 Waypoint 부터 끝 Waypoint 까지 이동하지만, 끝 Waypoint 에
          도달하면 Destroy 되지않고 첫 Waypoint 를 향해 이동합니다.
    - Tile Spawn Type
        - 타일의 생성 방법을 설정합니다.
        - ONLYONE 은 단 하나의 타일만 생성합니다.
        - INFINITE 는 타일을 정해진 값을 따라 계속 생성합니다.
    - Tile Move Speed
        - 타일이 이동하는 속도를 설정합니다.
    - Tile Life Time
        - 타일이 끝 Waypoint 에 도달하고 Deatroy 되기까지의 시간을 설정합니다.
    - Tile Spawn Interval
        - 타일 생성 간격을 조절합니다.