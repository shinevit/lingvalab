import React, {Component} from 'react';
import Carousel from 'react-bootstrap/Carousel'

class CarouselMain extends Component {

    render() {        
        let temp = this.props.images;

        temp.map(
            (element, elementKey) => {                
                 return true;
             }
         )

        let slides = [];
        
        temp.map(
            (element, elementKey) => {                               
                slides.push(
                    <Carousel.Item key={elementKey}>
                        <img                            
                            className="d-block w-100"
                            src={element.imgUrl}
                            alt={element.imgHead}
                        />
                        <Carousel.Caption>
                            <h3>{element.imgHead}</h3>
                            <p>{element.imgText}</p>
                        </Carousel.Caption>
                    </Carousel.Item>
                )
                return true;
            }
        )

        return(            
            <Carousel interval="1000" indicators="false">
                {slides}
            </Carousel>
        )
    }

    
}

export default CarouselMain;