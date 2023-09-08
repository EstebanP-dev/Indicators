import "./single.scss"

type Props = {
    id: number;
}

const Single = () => {
  return (
    <div className="single">
      <div className="view">
        <div className="info">
            <div className="topinfo">
                <img src="https://images.pexels.com/photos/5791753/pexels-photo-5791753.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1" alt="Avatar image" />
                <h1>Estaban Navia</h1>
                <button>Actualizar</button>
            </div>
            <div className="details">
                <div className="item">
                    <span className="itemTitle"></span>
                    <span className="itemValue"></span>
                </div>
            </div>
        </div>
        <div className="charts"></div>
      </div>
      <hr />
      <div className="activities">
        <h2>Ultimas actividades</h2>
        <ul>
            <li>
                <div>
                    <p>Actividad de prueba</p>
                    <time>Hace 4 días</time>
                </div>
            </li>
        </ul>
      </div>
    </div>
  )
}

export default Single